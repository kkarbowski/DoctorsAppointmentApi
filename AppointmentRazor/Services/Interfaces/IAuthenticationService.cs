using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services.Interfaces
{
    public enum Role
    {
        PATIENT,
        DOCTOR
    }

    public class AuthenticationReponse
    {
        public bool WasAuthenticationCorrect { get; set; }

        public string Token { get; set; }

        public Role Role { get; set; }
    }
    public interface IAuthenticationService
    {
        public bool Register(User user);

        public AuthenticationReponse Login(string username, string password);
    }
}
