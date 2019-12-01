using AppointmentApi.Authorization;
using AppointmentApi.Business;
using AppointmentModel;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentApi.Controllers
{
    //[Authorize]
    //[ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IPatientBusiness _patientBusiness;
        private readonly IPatientAuthorization _patientAuthorization;

        public PatientController(ILogger<PatientController> logger, IPatientBusiness patientBusiness, IPatientAuthorization patientAuthorization)
        {
            _logger = logger;
            _patientBusiness = patientBusiness;
            _patientAuthorization = patientAuthorization;
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet]
        public IActionResult GetPatients()
        {
            var patients = _patientBusiness.GetPatients();

            return Ok(patients);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpGet("{patientId}/Appointment")]
        public IActionResult GetPatientAppointmenta(int patientId)
        {
            var patientAppointment = _patientBusiness.GetPatientAppointments(patientId);

            return Ok(patientAppointment);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet("{patientId}")]
        public IActionResult GetPatient(int patientId)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(patientId, User))
                return Unauthorized();
            
            var patient = _patientBusiness.GetPatient(patientId);

            return Ok(patient);
        }

        //[AllowAnonymous]
        //[Authorize(Roles = Role.Doctor)]
        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient patient)
        {
            var newPatient = _patientBusiness.AddPatient(patient);
            if (newPatient == null)
                return BadRequest();
            return Created(nameof(GetPatient), newPatient);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpPut("{patientId}")]
        public IActionResult UpdatePatient(int patientId, [FromBody] Patient patient)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(patient.UserId, User))
                return Unauthorized();
            if (patientId != patient.UserId)
                return Forbid();

            var updatedPatient = _patientBusiness.UpdatePatient(patient);
            if (updatedPatient == null)
                return BadRequest();
            return Created(nameof(GetPatient), updatedPatient);
        }

    }
}
