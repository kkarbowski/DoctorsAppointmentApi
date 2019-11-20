using AppointmentModel.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentModel
{
    public class Patient : User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
