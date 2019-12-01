using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppointmentModel.Model
{
    public class Reason
    {
        [Key]
        public int ReasonId { get; set; }

        public Dictionary<string, string> LangReasonDictionary { get; set; }

        public virtual ICollection<Appointment2Reason> ReasonAppointments { get; set; }

    }
}
