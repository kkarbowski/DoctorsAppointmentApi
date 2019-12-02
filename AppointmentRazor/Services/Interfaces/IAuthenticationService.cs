using AppointmentModel;
using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services.Interfaces
{
    public class AuthenticationReponse
    {
        public bool WasAuthenticationCorrect { get; set; }

        public string Token { get; set; }

        public List<string> Roles { get; set; }

        public int? PatientId { get; set; }
    }
    public interface IAuthenticationService
    {
        public Task<bool> Register(Patient patient);

        public Task<AuthenticationReponse> Login(User user);
    }
}
