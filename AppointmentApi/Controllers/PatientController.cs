using AppointmentApi.Authorization;
using AppointmentApi.Business;
using AppointmentModel;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Serilog;
using AppointmentApi.Filters.Action;

namespace AppointmentApi.Controllers
{
    //[Authorize]
    //[ApiController]
    [ServiceFilter(typeof(LoggingFilter))]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBusiness _patientBusiness;
        private readonly IPatientAuthorization _patientAuthorization;

        public PatientController(IPatientBusiness patientBusiness, IPatientAuthorization patientAuthorization)
        {
            _patientBusiness = patientBusiness;
            _patientAuthorization = patientAuthorization;
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet]
        public IActionResult GetPatients()
        {
            Log.Information("Getting information about patients");
            var patients = _patientBusiness.GetPatients();

            return Ok(patients);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet("{patientId}/Appointment")]
        public IActionResult GetPatientAppointments(int patientId)
        {
            Log.Information("Getting information about appointments of specific patient");
            var patientAppointment = _patientBusiness.GetPatientAppointments(patientId);

            return Ok(patientAppointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet("{patientId}")]
        public IActionResult GetPatient(int patientId)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(patientId, User))
            {
                Log.Error("You are not authorized to do this");
                return Unauthorized();
            }

            Log.Information("Getting information about patient");
            var patient = _patientBusiness.GetPatient(patientId);

            return Ok(patient);
        }

        //[AllowAnonymous]
        //[Authorize(Roles = Role.Doctor)]
        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient patient)
        {
            Log.Information($"Adding new patient {patient.FullName}");
            var newPatient = _patientBusiness.AddPatient(patient);
            if (newPatient == null)
            {
                Log.Error("Bad Request - patient was not added");
                return BadRequest();
            }

            Log.Information("Patient was added");
            return Created(nameof(GetPatient), newPatient);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpPut("{patientId}")]
        public IActionResult UpdatePatient(int patientId, [FromBody] Patient patient)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(patient.UserId, User))
            {
                Log.Error("You are not authorized to do this");
                return Unauthorized();
            }
            if (patientId != patient.UserId)
            {
                Log.Error("Patient ID does not match");
                return Forbid();
            }

            Log.Information($"Updating information about patient {patient.FullName}");
            var updatedPatient = _patientBusiness.UpdatePatient(patient);
            if (updatedPatient == null)
            {
                Log.Error("Bad Request - patient was not updated");
                return BadRequest();
            }

            Log.Information("Patient was updated");
            return Created(nameof(GetPatient), updatedPatient);
        }

    }
}
