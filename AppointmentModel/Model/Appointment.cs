using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppointmentModel.Model
{
    public enum AppointmentSetResponse
    {
        /// <summary>
        /// Apointment was set correctly
        /// </summary>
        CORRECT,
        /// <summary>
        /// Picked date is not available
        /// </summary>
        DATE_NOT_AVAILABLE,
        /// <summary>
        /// Picked doctor is not available
        /// </summary>
        DOCTOR_NOT_AVAILABLE
    }

    [Serializable]
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public Patient Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Doctor Doctor { get; set; }
        public string Description { get; set; }
        [DefaultValue(false)]
        public bool IsCanceled { get; set; }
        public virtual ICollection<Appointment2Reason> AppointmentReasons { get; set; }

        public Appointment RemoveReferenceLoop()
        {
            if (AppointmentReasons != null)
                foreach (var ar in AppointmentReasons)
                    ar.RemoveReferenceLoop();

            return this;
        }
    }
}
