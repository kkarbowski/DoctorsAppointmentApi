using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Localization;
using AppointmentRazor.Utilities.Localization.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ICultureLocalizer _cultureLocalizer;
        private readonly IAuthenticationService authenticationService;

        public LoginModel(ICultureLocalizer cultureLocalizer, IAuthenticationService authenticationService)
        {
            _cultureLocalizer = cultureLocalizer;
            this.authenticationService = authenticationService;
        }

        public class _LoginForm
        {
            [Required(ErrorMessage = "Please enter value for {0}")]
            public string Username { get; set; }

            [Required(ErrorMessage = "Please enter value for {0}")]
            public string Password { get; set; }
        }

        [BindProperty]
        public _LoginForm LoginForm { get; set; }

        public string Msg { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Login = LoginForm.Username,
                    Password = LoginForm.Password
                };
                var authenticationReponse = await authenticationService.Login(user);

                if(authenticationReponse.WasAuthenticationCorrect)
                {
                    HttpContext.Response.Redirect(CurentCultureUtils.GetCurrentCultureLink("Index"));

                    return null;
                }
                Msg = _cultureLocalizer.Text("InvalidLogin");
            }

            return Page();
        }
    }
}