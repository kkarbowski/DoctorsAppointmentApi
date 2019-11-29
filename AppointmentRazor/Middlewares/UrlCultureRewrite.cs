using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Middlewares
{
    public class UrlCultureRewrite
    {
        public static void RedirectRequests(RewriteContext context)
        {
            var request = context.HttpContext.Request;
            var path = request.Path.Value;

            var currentCulture = CurentCultureUtils.GetCurrentCulture();

            if (!CurentCultureUtils.SupportedCultures.Contains(path.Split("/")[1]))
            {
                context.HttpContext.Response.Redirect($"/{currentCulture}{ request.Path.Value }");
            }

        }
    }
}
