using AppointmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppointmentApi.Business
{
    public interface IPatientBusiness
    {
        public Patient[] GetPatients();
        public Patient AddPatient(Patient patient);
        public Patient UpdatePatient(Patient patient);
        public Patient GetPatient(int patientId);
    }
}
