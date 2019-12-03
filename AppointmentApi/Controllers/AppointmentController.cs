using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Authorization;
using AppointmentApi.Business;
using AppointmentApi.Filters.Action;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AppointmentApi.Controllers
{
    [ServiceFilter(typeof(LoggingFilter))]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentBusiness _appointmentBusiness;
        private readonly IPatientAuthorization _patientAuthorization;

        public AppointmentController(IAppointmentBusiness appointmentBusiness, IPatientAuthorization patientAuthorization)
        {
            _appointmentBusiness = appointmentBusiness;
            _patientAuthorization = patientAuthorization;
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet]
        public IActionResult GetAppointments()
        {
            Log.Information("Getting information about appointments");
            var appointments = _appointmentBusiness.GetAppointments();

            return Ok(appointments);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet("{appointmentId}")]
        public IActionResult GetAppointment(int appointmentId)
        {
            Log.Information("Getting information about appointment");
            var appointment = _appointmentBusiness.GetAppointment(appointmentId);

            return Ok(appointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpPost]
        public IActionResult AddAppointment([FromBody] Appointment appointment)
        {
            //if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(appointment.Patient.UserId, User))
            //    return Unauthorized();

            Log.Information($"Adding new appointment for {appointment.AppointmentDate.Date}");
            var newAppointment = _appointmentBusiness.AddAppointment(appointment);
            if (newAppointment == null)
            {
                Log.Warning("Bad Request - appointment was not added");
                return BadRequest();
            }
            
            Log.Information("Appointment was added");
            return Created(nameof(GetAppointment), newAppointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpPut("{appointmentId}")]
        public IActionResult UpdateAppointment(int appointmentId, [FromBody] Appointment appointment)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(appointment.Patient.UserId, User))
                return Unauthorized();
            if (appointmentId != appointment.AppointmentId)
                return Forbid();

            Log.Information($"Updating appointment for date ${appointment.AppointmentDate.Date} with " +
                $"patient ${appointment.Patient.FullName} " +
                $"and doctor ${appointment.Doctor.FullName}");
            var updatedAppointment = _appointmentBusiness.UpdateAppointment(appointment);
            if (updatedAppointment == null)
            {
                Log.Warning("Bad Request - appointment was not updated");
                return BadRequest();
            }

            Log.Information("Doctor was updated");
            return Created(nameof(GetAppointment), updatedAppointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpDelete("{appointmentId}")]
        public IActionResult CancelAppointment(int appointmentId)
        {
            var appointment = _appointmentBusiness.GetAppointment(appointmentId);
            appointment.IsCanceled = true;

            return UpdateAppointment(appointmentId, appointment);
        }
    }
}
