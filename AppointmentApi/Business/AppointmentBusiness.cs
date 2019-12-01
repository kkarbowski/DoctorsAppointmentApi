using AppointmentApi.Controllers;
using AppointmentApi.DataAccess;
using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Business
{
    public class AppointmentBusiness : IAppointmentBusiness
    {
        private readonly IAppointmentDataAccess _appointmentDataAccess;

        public AppointmentBusiness(IAppointmentDataAccess appointmentDataAccess)
        {
            _appointmentDataAccess = appointmentDataAccess;
        }

        public Appointment AddAppointment(Appointment appointment)
        {
            return UpdateAppointment(appointment);
        }

        public Appointment GetAppointment(int appointmentId)
        {
            return _appointmentDataAccess.GetAppointment(appointmentId);
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return _appointmentDataAccess.GetAppointments();
        }

        public Appointment UpdateAppointment(Appointment appointment)
        {
            try
            {
                return _appointmentDataAccess.UpdateAppointment(appointment);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
