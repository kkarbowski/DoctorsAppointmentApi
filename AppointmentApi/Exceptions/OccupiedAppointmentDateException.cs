using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Exceptions
{
    public class OccupiedAppointmentDateException : Exception
    {
        public OccupiedAppointmentDateException(DateTime appointmentDate, int doctorId)
            : base($"Appointment date '{appointmentDate}' for doctor {doctorId} is already occupied.")
        {
        }
    }
}
