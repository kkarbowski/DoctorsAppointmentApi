using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentModel
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public string Mail { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public DateTime DateTimeAdd { get; set; }
    }
}
