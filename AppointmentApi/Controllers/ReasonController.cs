using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Business;
using AppointmentApi.Filters.Action;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace AppointmentApi.Controllers
{
    [ServiceFilter(typeof(LoggingFilter))]
    [Route("[controller]")]
    public class ReasonController : ControllerBase
    {
        private readonly IReasonBusiness _reasonBusiness;

        public ReasonController(IReasonBusiness reasonBusiness)
        {
            _reasonBusiness = reasonBusiness;
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet]
        public IActionResult GetReasons()
        {
            Log.Information("Getting information about reasons");
            var reasons = _reasonBusiness.GetReasons();

            return Ok(reasons);
        }

        //[Authorize(Roles = Role.Patient)]
        [HttpGet("{reasonId}")]
        public IActionResult GetReason(int reasonId)
        {
            Log.Information("Getting information about reason");
            var reason = _reasonBusiness.GetReason(reasonId);

            return Ok(reason);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPost]
        public IActionResult AddReason([FromBody] Reason reason)
        {
            Log.Information("Adding new reason");
            var newReason = _reasonBusiness.AddReason(reason);
            if (newReason == null)
            {
                Log.Warning("Bad Request - reason was not added");
                return BadRequest();
            }

            Log.Information("Reason was added");
            return Created(nameof(GetReason), newReason);
        }

        //[Authorize(Roles = Role.Doctor)]
        [HttpPut("{reasonId}")]
        public IActionResult UpdateReason(int reasonId, [FromBody] Reason reason)
        {
            if (reason.ReasonId != reasonId) {
                return Forbid();
            }

            Log.Information("Updating reasons");
            var updatedReason = _reasonBusiness.UpdateReason(reason);
            if (updatedReason == null)
            {
                Log.Warning("Bad Request - reason was not updated");
            }

            Log.Information("Reason was updated");
            return Created(nameof(GetReason), updatedReason);
        }
    }
}
