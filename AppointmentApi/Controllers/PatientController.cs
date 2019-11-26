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

        public PatientController(ILogger<PatientController> logger, IPatientBusiness patientBusiness)
        {
            _logger = logger;
            _patientBusiness = patientBusiness;
        }

        [Authorize(Roles = Role.Doctor)]
        [HttpGet]
        public IActionResult GetPatients()
        {
            var patients = _patientBusiness.GetPatients();

            return Ok(patients);
        }

        [Authorize(Roles = Role.Patient)]
        [HttpGet("{patientId}")]
        public IActionResult GetPatient(int patientId)
        {
            if (User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value == patientId.ToString())
                return Forbid();
            
            var patient = _patientBusiness.GetPatient(patientId);

            return Ok(patient);
        }

        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {
            var newPatient = _patientBusiness.AddPatient(patient);
            if (newPatient == null)
                return BadRequest();
            return Created(nameof(GetPatient), newPatient);
        }

    }
}
