using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Pages
{
    public class RegisterModel : PageModel
    {
        public class _RegistrationForm
        {
            [Required(ErrorMessage = "Please enter value for {0}")]
            [StringLength( maximumLength: 30, MinimumLength = 1,
                ErrorMessage = "'{0}' must be at least {2} and maximum {1} characters")]
            [Display(Name = "Username")]
            public string Login { get; set; }
            [Required(ErrorMessage = "Please enter value for {0}")]
            [StringLength(maximumLength: 12, MinimumLength = 6,
                ErrorMessage = "'{0}' must be at least {2} and maximum {1} characters")]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        [BindProperty]
        public _RegistrationForm RegistrationForm { get; set; }

        private readonly CultureLocalizer _cultureLocalizer;
        private readonly IAuthenticationService authenticationService;

        public RegisterModel([FromServices] CultureLocalizer cultureLocalizer, IAuthenticationService authenticationService)
        {
            _cultureLocalizer = cultureLocalizer;
            this.authenticationService = authenticationService;
        }

        public string Msg { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid)
            {
                var wasRegistrationCorrect = await authenticationService
                        .Register(new Patient() 
                        { 
                            Login = RegistrationForm.Login, 
                            Password = RegistrationForm.Password,
                        });
                if(wasRegistrationCorrect)
                {
                    return Redirect("Login");
                }

                Msg = _cultureLocalizer.Text("Registration failed");
            }

            return Page();
        }
    }
}