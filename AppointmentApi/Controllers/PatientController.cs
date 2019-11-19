using AppointmentApi.Business;
using AppointmentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetPatients()
        {
            var patients = PatientBusiness.GetPatients();

            return Ok(patients);
        }

        [HttpGet("/{patientId}")]
        public IActionResult GetPatient(int patientId)
        {
            var patient = PatientBusiness.GetPatient(patientId);

            return Ok(patient);
        }

        [HttpPost]
        public IActionResult AddPatient(Patient patient)
        {
            var newPatient = PatientBusiness.AddPatient(patient);

            return Created(nameof(GetPatient), newPatient);
        }

    }
}
