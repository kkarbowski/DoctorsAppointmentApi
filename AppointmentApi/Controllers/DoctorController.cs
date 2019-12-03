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
using Serilog;

namespace AppointmentApi.Controllers
{
    //[Authorize]
    //[ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly ILogger<DoctorController> _logger;
        private readonly IDoctorBusiness _doctorBusiness;

        public DoctorController(ILogger<DoctorController> logger, IDoctorBusiness doctorBusiness)
        {
            _logger = logger;
            _doctorBusiness = doctorBusiness;
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet]
        public IActionResult GetDoctors()
        {
            var doctors = _doctorBusiness.GetDoctors();

            return Ok(doctors);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet("{doctorId}")]
        public IActionResult GetDoctor(int doctorId)
        {           
            var doctor = _doctorBusiness.GetDoctor(doctorId);

            return Ok(doctor);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPost]
        public IActionResult AddDoctor([FromBody] Doctor doctor)
        {
            var newDoctor = _doctorBusiness.AddDoctor(doctor);
            if (newDoctor == null)
                return BadRequest();
            return Created(nameof(GetDoctor), newDoctor);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPut("{doctorId}")]
        public IActionResult UpdateDoctor(int doctorId, [FromBody] Doctor doctor)
        {
            if (doctorId != doctor.UserId)
                return Forbid();

            var updatedDoctor = _doctorBusiness.UpdateDoctor(doctor);
            if (updatedDoctor == null)
                return BadRequest();
            return Created(nameof(GetDoctor), updatedDoctor);
        }

    }
}
