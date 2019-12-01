using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AppointmentModel.Model
{
    public class Appointment2Reason
    {
        [ForeignKey(nameof(AppointmentId))]
        public int AppointmentId { get; set; }
        [ForeignKey(nameof(ReasonId))]
        public int ReasonId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Reason Reason { get; set; }

        public Appointment2Reason RemoveReferenceLoop()
        {
            Appointment = default;
            Reason = default;
            return this;
        }
    }
}
