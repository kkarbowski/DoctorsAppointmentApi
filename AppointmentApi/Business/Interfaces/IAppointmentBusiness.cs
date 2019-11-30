using AppointmentModel.Model;
using System.Collections.Generic;

namespace AppointmentApi.Business
{
    public interface IAppointmentBusiness
    {
        public Appointment AddAppointment(Appointment appointment);
        public Appointment GetAppointment(int appointmentId);
        public Appointment UpdateAppointment(Appointment appointment);
        public IEnumerable<Appointment> GetAppointments();
    }
}