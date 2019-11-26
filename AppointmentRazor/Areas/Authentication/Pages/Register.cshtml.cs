using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
            [StringLength( maximumLength: 50, MinimumLength = 7,
                ErrorMessage = "'{0}' must be at least {1} and maximum {2} characters")]
            [Display(Name = "Username")]
            public string Login { get; set; }
            [Required(ErrorMessage = "Please enter value for {0}")]
            [StringLength(maximumLength: 50, MinimumLength = 7,
                ErrorMessage = "'{0}' must be at least {1} and maximum {2} characters")]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }

        [BindProperty]
        public _RegistrationForm RegistrationForm { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                return Redirect("Login");
            }

            return Page();
        }
    }
}