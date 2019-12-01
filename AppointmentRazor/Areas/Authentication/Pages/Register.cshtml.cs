using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization;
using AppointmentRazor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegistrationForm RegistrationForm { get; set; }

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
            if (RegistrationForm.Birthday > DateTime.Today)
            {
                ModelState.AddModelError("RegistrationForm.Birthday", _cultureLocalizer.Text("Date must be from the past"));
                return Page();
            }

            if (ModelState.IsValid)
            {
                var wasRegistrationCorrect = await authenticationService
                        .Register(new Patient() 
                        { 
                            Login = RegistrationForm.Login, 
                            Password = RegistrationForm.Password,
                            BirthDate = RegistrationForm.Birthday.Value,
                            FirstName = RegistrationForm.FirstName,
                            LastName = RegistrationForm.LastName,
                            FullName = $"{RegistrationForm.FirstName} {RegistrationForm.LastName}",
                            Mail = RegistrationForm.Mail,
                            Phone = RegistrationForm.Phone
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