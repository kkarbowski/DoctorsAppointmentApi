using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentModel.Model
{
    public class Doctor : User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }
    }
}
