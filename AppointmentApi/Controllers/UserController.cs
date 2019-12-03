using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentApi.Business.Interfaces;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppointmentApi.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            Log.Information($"Trying to login {user.Login}");
            var token = _userBusiness.Login(user);
            if (token == null)
            {
                return Unauthorized();
            }
            Log.Information("Successfully logged in");
            return Ok(token);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            Log.Information($"Trying to register user {user.Login}");
            var dbUser = _userBusiness.Register(user);
            if (dbUser == null)
            {
                Log.Warning("Bad Request - user was not registered");
                return BadRequest();
            }
            Log.Information($"Successfully registered user {user.Login}");
            return Created(nameof(Login), dbUser);
        }
    }
}
