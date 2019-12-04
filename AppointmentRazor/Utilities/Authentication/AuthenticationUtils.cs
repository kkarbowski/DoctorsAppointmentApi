using AppointmentRazor.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRazor.Utilities.Authentication
{
    public static class AuthenticationUtils
    {
        private static readonly string tokenSessionKey = "token";
        private static readonly string rolesSessionKey = "roles";
        private static readonly string patientIdSessionKey = "patientId";

        public static void SaveUserToSession(HttpContext httpContext, AuthenticationReponse authenticationReponse)
        {
            if(authenticationReponse.WasAuthenticationCorrect)
            {
                httpContext.Session.SetString(tokenSessionKey, authenticationReponse.Token);

                if(authenticationReponse.Roles != null || authenticationReponse.Roles.Count > 0)
                {
                    string roles = string.Join(",", authenticationReponse.Roles);
                    httpContext.Session.SetString(rolesSessionKey, roles);
                }

                if(authenticationReponse.PatientId.HasValue)
                {
                    httpContext.Session.SetInt32(patientIdSessionKey, authenticationReponse.PatientId.Value);
                }
            }
  
        }

        public static string GetUserToken(HttpContext httpContext)
        {
            return httpContext.Session.GetString(tokenSessionKey);
        }

        public static bool IsUserLoggedIn(HttpContext httpContext)
        {
            return httpContext.Session.TryGetValue(tokenSessionKey, out _);
        }

        public static bool IsUserInRole(HttpContext httpContext, string role)
        {
            byte[] sessionRole;
            if(httpContext.Session.TryGetValue(rolesSessionKey, out sessionRole))
            {
                string[] sessionRoleString = Encoding.UTF8.GetString(sessionRole, 0, sessionRole.Length).Split(",");
                return sessionRoleString.Contains(role);
            }

            return false;
        }

        public static int? GetPatientId(HttpContext httpContext)
        {
            return httpContext.Session?.GetInt32(patientIdSessionKey);
        }
    }
}
