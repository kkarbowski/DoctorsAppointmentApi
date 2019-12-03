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

namespace AppointmentApi.Controllers
{
    //[Authorize]
    //[ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorBusiness _doctorBusiness;

        public DoctorController(IDoctorBusiness doctorBusiness)
        {
            _doctorBusiness = doctorBusiness;
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet]
        public IActionResult GetDoctors()
        {
            Log.Debug("GET doctors");
            Log.Information("Getting information about doctors");
            var doctors = _doctorBusiness.GetDoctors();

            return Ok(doctors);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet("{doctorId}")]
        public IActionResult GetDoctor(int doctorId)
        {
            Log.Debug($"GET doctor, doctorId=${doctorId}");
            Log.Information("Getting information about doctor");
            var doctor = _doctorBusiness.GetDoctor(doctorId);

            return Ok(doctor);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPost]
        public IActionResult AddDoctor([FromBody] Doctor doctor)
        {
            Log.Debug($"POST doctor, FullName={doctor.FullName}, " +
                $"Login={doctor.Login}");
            Log.Information($"Adding new doctor {doctor.FullName}");
            var newDoctor = _doctorBusiness.AddDoctor(doctor);
            if (newDoctor == null)
            {
                Log.Error("Bad Request - doctor was not added properly");
                return BadRequest();
            }

            Log.Information("Doctor was added");
            return Created(nameof(GetDoctor), newDoctor);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPut("{doctorId}")]
        public IActionResult UpdateDoctor(int doctorId, [FromBody] Doctor doctor)
        {
            if (doctorId != doctor.UserId)
            {
                Log.Error("Doctor ID does not match");
                return Forbid();
            }

            Log.Debug($"PUT doctor with Id=${doctorId}");
            Log.Information($"Updating information about doctor {doctor.FullName}");
            var updatedDoctor = _doctorBusiness.UpdateDoctor(doctor);
            if (updatedDoctor == null)
            {
                Log.Error("Bad Request - doctor was not updated");
                return BadRequest();
            }

            Log.Information("Doctor was updated");
            return Created(nameof(GetDoctor), updatedDoctor);
        }

    }
}
