using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Business.Interfaces;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentApi.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserBusiness _userBusiness;

        public UserController(ILogger<UserController> logger, IUserBusiness userBusiness)
        {
            _logger = logger;
            _userBusiness = userBusiness;
        }


        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var token = _userBusiness.Login(user);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            var dbUser = _userBusiness.Register(user);
            if (dbUser == null)
                return BadRequest();
            return Created(nameof(Login), dbUser);
        }
    }
}
