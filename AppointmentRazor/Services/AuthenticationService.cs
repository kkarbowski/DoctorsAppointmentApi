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
        public AuthenticationReponse Login(string username, string password)
        {
            //TODO: Implement me
            return new AuthenticationReponse() 
                { WasAuthenticationCorrect = true, Role = Role.PATIENT, Token = "sometoken" };
        }

        public bool Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
