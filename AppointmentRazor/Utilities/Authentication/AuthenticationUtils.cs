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
        public static bool IsUserLoggedIn(HttpContext httpContext)
        {
            return httpContext.Session.TryGetValue("token", out _);
        }

        public static bool IsUserInRole(HttpContext httpContext, string role)
        {
            byte[] sessionRole;
            if(httpContext.Session.TryGetValue("roles", out sessionRole))
            {
                string[] sessionRoleString = Encoding.UTF8.GetString(sessionRole, 0, sessionRole.Length).Split(",");
                return sessionRoleString.Contains(role);
            }

            return false;
        }
    }
}
