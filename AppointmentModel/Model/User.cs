using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppointmentModel.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 1)]
        public string Login { get; set; }

        [Required]
        [StringLength(maximumLength: 12 ,MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        public DateTime DateTimeAdd { get; set; }
    }
}
