using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public bool Register(User user);

        public bool Login(string username, string password);
    }
}
