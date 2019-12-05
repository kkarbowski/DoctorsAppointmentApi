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
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AppointmentApi.Controllers
{
    [ServiceFilter(typeof(LoggingFilter))]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientBusiness _patientBusiness;
        private readonly IPatientAuthorization _patientAuthorization;
        private readonly IDistributedCache _distributedCache;

        public PatientController(IPatientBusiness patientBusiness, IPatientAuthorization patientAuthorization,
            IDistributedCache distributedCache)
        {
            _patientBusiness = patientBusiness;
            _patientAuthorization = patientAuthorization;
            _distributedCache = distributedCache;
        }

        [Authorize(Roles = Role.Doctor)]
        [HttpGet]
        public IActionResult GetPatients()
        {
            Log.Information("Getting information about patients");
            var cacheKey = "Patients";

            var patientsData = _distributedCache.GetString(cacheKey);
            if (!string.IsNullOrEmpty(patientsData))
            {
                Log.Information("Using cached data");
                return Ok(JsonConvert.DeserializeObject<List<Patient>>(patientsData));
            }
            var patients = _patientBusiness.GetPatients();

            DistributedCacheEntryOptions patientsExpiration = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration =  DateTimeOffset.UtcNow.AddSeconds(30)
            };
            _distributedCache.SetString(cacheKey, JsonConvert.SerializeObject(patients), patientsExpiration);

            Log.Information("Added new data to cache");

            return Ok(patients);
        }

        [Authorize(Roles = Role.Doctor_Patient)]
        [HttpGet("{patientId}/Appointment")]
        public IActionResult GetPatientAppointments(int patientId)
        {
            Log.Information("Getting information about appointments of specific patient");
            var patientAppointment = _patientBusiness.GetPatientAppointments(patientId);

            return Ok(patientAppointment);
        }

        [Authorize(Roles = Role.Doctor_Patient)]
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

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddPatient([FromBody] Patient patient)
        {
            Log.Information($"Adding new patient {patient.FullName}");
            var newPatient = _patientBusiness.AddPatient(patient);
            if (newPatient == null)
            {
                Log.Warning("Bad Request - patient was not added");
                return BadRequest();
            }

            Log.Information("Patient was added");
            _distributedCache.Remove("Patients");
            return Created(nameof(GetPatient), newPatient);
        }

        [Authorize(Roles = Role.Doctor_Patient)]
        [HttpPut("{patientId}")]
        public IActionResult UpdatePatient(int patientId, [FromBody] Patient patient)
        {
            if (!User.IsInRole(Role.Doctor) && !_patientAuthorization.IsPatientOwnAccount(patient.UserId, User))
            {
                Log.Warning("You are not authorized to do this");
                return Unauthorized();
            }
            if (patientId != patient.UserId)
            {
                return Forbid();
            }

            Log.Information($"Updating information about patient {patient.FullName}");
            var updatedPatient = _patientBusiness.UpdatePatient(patient);
            if (updatedPatient == null)
            {
                Log.Warning("Bad Request - patient was not updated");
                return BadRequest();
            }

            Log.Information("Patient was updated");
            _distributedCache.Remove("Patients");
            return Created(nameof(GetPatient), updatedPatient);
        }
    }
}
