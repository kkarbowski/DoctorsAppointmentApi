using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Tools.Interfaces
{
    public interface ITokenGenerator
    {
        public Token GenerateToken(User user);
    }
}
