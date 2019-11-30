using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Middlewares
{
    public class UrlAuthenticationRewrite
    {
        public static void RedirectRequests(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var path = request.Path.Value;

            var publicRoutes = new List<string>
            {
               "Index",
               "Login",
               "Register"
            };

            var isUserLoggedIn = AuthenticationUtils.IsUserLoggedIn(context.HttpContext);

            bool isRoutePublic = false;
            publicRoutes.ForEach(route =>
            {
                if(path.Contains(route))
                {
                    isRoutePublic = true;
                }
            });

            if (!isUserLoggedIn && !isRoutePublic)
            {
                context.HttpContext.Response.Redirect(CurentCultureUtils.GetCurrentCultureLink("Authentication/Login"));
            }

        }
    }
}
