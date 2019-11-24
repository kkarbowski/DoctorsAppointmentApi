using AppointmentApi.Business;
using AppointmentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IActionResult GetPatients()
        {
            var patients = _patientBusiness.GetPatients();

            return Ok(patients);
        }
        
        [HttpGet("{patientId}")]
        public IActionResult GetPatient(int patientId)
        {
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
