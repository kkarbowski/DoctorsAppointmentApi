using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly CultureLocalizer _cultureLocalizer;
        private readonly IAuthenticationService authenticationService;

        public LoginModel([FromServices] CultureLocalizer cultureLocalizer, IAuthenticationService authenticationService)
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
                var authenticationReponse = 
                    await authenticationService.Login(
                        new User() 
                        { 
                            Login = LoginForm.Username, 
                            Password = LoginForm.Password 
                        });

                if(authenticationReponse.WasAuthenticationCorrect)
                {
                    HttpContext.Session.SetString("token", authenticationReponse.Token);

                    string roles = string.Join(",", authenticationReponse.Roles);
                    HttpContext.Session.SetString("roles", roles);

                    HttpContext.Response.Redirect(CurentCultureUtils.GetCurrentCultureLink("Index"));

                    return null;
                }
                Msg = _cultureLocalizer.Text("InvalidLogin");
            }

            return Page();
        }
    }
}