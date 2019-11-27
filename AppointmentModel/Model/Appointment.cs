using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentModel.Model
{
    public class Appointment
    {
        public Patient Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public User Doctor { get; set; }
        public string Description { get; set; }

    }
}
