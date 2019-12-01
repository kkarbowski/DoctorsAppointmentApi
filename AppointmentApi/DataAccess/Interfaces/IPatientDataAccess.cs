using AppointmentModel;
using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.DataAccess.Interfaces
{
    public interface IPatientDataAccess
    {
        public Patient GetPatient(int patientId);
        public Patient[] GetPatients();
        public Patient UpdatePatient(Patient patient);
        IEnumerable<Appointment> GetPatientAppointments(int patientId);
    }
}
