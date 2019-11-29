using System;
using System.Collections.Generic;
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

    public class Appointment
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Doctor Doctor { get; set; }
        public string Description { get; set; }
        public List<Dictionary<string, string>> Reasons { get; set; }
    }
}
