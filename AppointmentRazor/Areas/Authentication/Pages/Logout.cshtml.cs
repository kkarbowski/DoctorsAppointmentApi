using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Areas.Authentication.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Session.Remove("token");
            HttpContext.Session.Remove("role");

            HttpContext.Response.Redirect(CurentCultureUtils.GetCurrentCultureLink("Authentication/Login"));
        }
    }
}
