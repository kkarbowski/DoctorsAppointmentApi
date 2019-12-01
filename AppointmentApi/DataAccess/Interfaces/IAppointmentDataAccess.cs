using System.Collections.Generic;
using AppointmentModel.Model;

namespace AppointmentApi.DataAccess
{
    public interface IAppointmentDataAccess
    {
        IEnumerable<Appointment> GetAppointments();
        Appointment GetAppointment(int appointmentId);
        Appointment UpdateAppointment(Appointment appointment);
    }
}