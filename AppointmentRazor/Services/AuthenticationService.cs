using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;

namespace AppointmentRazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
