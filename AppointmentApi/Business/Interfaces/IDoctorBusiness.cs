using AppointmentModel.Model;
using System.Collections.Generic;

namespace AppointmentApi.Business
{
    public interface IDoctorBusiness
    {
        IEnumerable<Doctor> GetDoctors();

        Doctor AddDoctor(Doctor doctor);

        Doctor GetDoctor(int doctorId);

        Doctor UpdateDoctor(Doctor doctor);
    }
}