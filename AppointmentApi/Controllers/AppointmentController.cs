using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Authorization;
using AppointmentApi.Business;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppointmentApi.Controllers
{
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;
        private readonly IAppointmentBusiness _appointmentBusiness;
        private readonly IPatientAuthorization _patientAuthorization;

        public AppointmentController(ILogger<AppointmentController> logger, IAppointmentBusiness appointmentBusiness, IPatientAuthorization patientAuthorization)
        {
            _logger = logger;
            _appointmentBusiness = appointmentBusiness;
            _patientAuthorization = patientAuthorization;
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet]
        public IActionResult GetAppointments()
        {
            var appointments = _appointmentBusiness.GetAppointments();

            return Ok(appointments);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet("{appointmentId}")]
        public IActionResult GetAppointment(int appointmentId)
        {
            var appointment = _appointmentBusiness.GetAppointment(appointmentId);

            return Ok(appointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpPost]
        public IActionResult AddAppointment(Appointment appointment)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(appointment.Patient.UserId, User))
                return Unauthorized();

            var newAppointment = _appointmentBusiness.AddAppointment(appointment);
            if (newAppointment == null)
                return BadRequest();
            return Created(nameof(GetAppointment), newAppointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpPut("{appointmentId}")]
        public IActionResult UpdateAppointment(int appointmentId, Appointment appointment)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(appointment.Patient.UserId, User))
                return Unauthorized();
            if (appointmentId != appointment.AppointmentId)
                return Forbid();

            var updatedAppointment = _appointmentBusiness.UpdateAppointment(appointment);
            if (updatedAppointment == null)
                return BadRequest();
            return Created(nameof(GetAppointment), updatedAppointment);
        }
    }
}
