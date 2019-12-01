using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Business;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppointmentApi.Controllers
{
    [Route("[controller]")]
    public class ReasonController : ControllerBase
    {
        private readonly ILogger<ReasonController> _logger;
        private readonly IReasonBusiness _reasonBusiness;

        public ReasonController(ILogger<ReasonController> logger, IReasonBusiness reasonBusiness)
        {
            _logger = logger;
            _reasonBusiness = reasonBusiness;
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet]
        public IActionResult GetReasons()
        {
            return Ok(_reasonBusiness.GetReasons());
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet("{reasonId}")]
        public IActionResult GetReason(int reasonId)
        {
            return Ok(_reasonBusiness.GetReason(reasonId));
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPost]
        public IActionResult AddReason([FromBody] Reason reason)
        {
            return Created(nameof(GetReason), _reasonBusiness.AddReason(reason));
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPut("{reasonId}")]
        public IActionResult UpdateReason(int reasonId, [FromBody] Reason reason)
        {
            if (reason.ReasonId != reasonId)
                return Forbid();
            return Created(nameof(GetReason), _reasonBusiness.UpdateReason(reason));
        }
    }
}
