using AppointmentModel;
using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services.Interfaces
{
    public interface IProfileService
    {
        public Task<Patient> GetPatient(int patientId);

        public Task<List<Patient>> GetAllPatients();

        public Task<Doctor> GetDoctor(int doctorId);
    }
}
