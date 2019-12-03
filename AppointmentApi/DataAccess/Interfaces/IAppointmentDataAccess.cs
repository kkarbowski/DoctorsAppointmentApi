using System;
using System.Collections.Generic;
using AppointmentModel.Model;

namespace AppointmentApi.DataAccess
{
    public interface IAppointmentDataAccess
    {
        IEnumerable<Appointment> GetAppointments();
        Appointment GetAppointment(int appointmentId);
        Appointment UpdateAppointment(Appointment appointment);
        Appointment AddAppointment(Appointment appointment);
        Appointment GetAppointmentForSpecificDateAndDoctor(DateTime appointmentDate, int doctorid);
    }
}