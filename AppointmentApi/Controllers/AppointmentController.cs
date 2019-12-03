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
using Serilog;

namespace AppointmentApi.Controllers
{
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
            Log.Debug("GET appointments");
            Log.Information("Getting information about appointments");
            var appointments = _appointmentBusiness.GetAppointments();

            return Ok(appointments);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet("{appointmentId}")]
        public IActionResult GetAppointment(int appointmentId)
        {
            Log.Debug($"GET appointment, appointmentId={appointmentId}");
            Log.Information("Getting information about appointment");
            var appointment = _appointmentBusiness.GetAppointment(appointmentId);

            return Ok(appointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpPost]
        public IActionResult AddAppointment([FromBody] Appointment appointment)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(appointment.Patient.UserId, User))
            {
                Log.Error("User unauthorized to add appointment");
                return Unauthorized();
            }

            Log.Debug($"POST appointment, AppointmentDate={appointment.AppointmentDate.Date}, " +
                $"Doctor.FullName={appointment.Doctor.FullName}, " +
                $"Patient.FullName={appointment.Patient.FullName}");
            Log.Information($"Adding new appointment for {appointment.AppointmentDate.Date}");
            var newAppointment = _appointmentBusiness.AddAppointment(appointment);
            if (newAppointment == null)
            {
                Log.Error("Bad Request - appointment was not added");
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
            {
                Log.Error("You are not authorized to do this");
                return Unauthorized();
            }
            if (appointmentId != appointment.AppointmentId)
            {
                Log.Error("Appointment ID does not match");
                return Forbid();
            }

            Log.Debug($"PUT appointment with Id=${appointmentId}");
            Log.Information($"Updating appointment for date ${appointment.AppointmentDate.Date} with " +
                $"patient ${appointment.Patient.FullName} " +
                $"and doctor ${appointment.Doctor.FullName}");
            var updatedAppointment = _appointmentBusiness.UpdateAppointment(appointment);
            if (updatedAppointment == null)
            {
                Log.Error("Bad Request - appointment was not updated");
                return BadRequest();
            }

            Log.Information("Doctor was updated");
            return Created(nameof(GetAppointment), updatedAppointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpDelete("{appointmentId}")]
        public IActionResult CancelAppointment(int appointmentId, [FromBody] Appointment appointment)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(appointment.Patient.UserId, User))
            {
                Log.Error("You are not authorized to do this");
                return Unauthorized();
            }
            if (appointmentId != appointment.AppointmentId)
            {
                Log.Error("Appointment ID does not match");
                return Forbid();
            }

            appointment.IsCanceled = true;
            Log.Debug($"DELETE appointment with Id=${appointmentId}");
            Log.Information($"Removing appointment for date ${appointment.AppointmentDate.Date} with " +
                $"patient ${appointment.Patient.FullName} " +
                $"and doctor ${appointment.Doctor.FullName}");
            var updatedAppointment = _appointmentBusiness.UpdateAppointment(appointment);
            if (updatedAppointment == null)
            {
                Log.Error("Bad request - appointment was not deleted");
                return BadRequest();
            }
            return Created(nameof(GetAppointment), updatedAppointment);
        }
    }
}
