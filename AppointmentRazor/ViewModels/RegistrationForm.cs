using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.ViewModels
{
    public class RegistrationForm
    {
        [Required(ErrorMessage = "Please enter value for {0}")]
        [StringLength(maximumLength: 30, MinimumLength = 1,
                ErrorMessage = "'{0}' must be at least {2} and maximum {1} characters")]
        [Display(Name = "Username")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please enter value for {0}")]
        [StringLength(maximumLength: 12, MinimumLength = 6,
            ErrorMessage = "'{0}' must be at least {2} and maximum {1} characters")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter value for {0}")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter value for {0}")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter value for {0}")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter value for {0}")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Not a valid Email address")]
        [Display(Name = "Email")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Please enter value for {0}")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthday")]
        public DateTime? Birthday { get; set; }
    }
}
