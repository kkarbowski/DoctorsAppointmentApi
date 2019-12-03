using AppointmentModel.Model;
using System.Collections.Generic;

namespace AppointmentApi.DataAccess
{
    public interface IDoctorDataAccess
    {
        Doctor UpdateDoctor(Doctor doctor);

        Doctor GetDoctor(int doctorId);

        IEnumerable<Doctor> GetDoctors();
        IEnumerable<Appointment> GetDoctorAppointments(int doctorId);
    }
}