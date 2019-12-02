using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppointmentModel.Model
{
    public abstract class Human : User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string FullName { get; set; }
    }
}
