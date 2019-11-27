using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentApi.Authorization
{
    public interface IPatientAuthorization
    {
        bool IsPatientOwnAccount(int patientId, ClaimsPrincipal claimsPrincipal);
    }
}
