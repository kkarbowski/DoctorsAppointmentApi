using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Database;
using AppointmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.DataAccess
{
    public class PatientDataAccess : IPatientDataAccess
    {
        private readonly AppDbContext _appDbContext;

        public PatientDataAccess(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Patient UpdatePatient(Patient patient)
        {
            var newPatient = _appDbContext.Patients.Update(patient);
            _appDbContext.SaveChanges();

            return newPatient.Entity;
        }

        public Patient GetPatient(int patientId)
        {
            return _appDbContext.Patients.Find(patientId);
        }

        public Patient[] GetPatients()
        {
            return _appDbContext.Patients.ToArray();
        }
    }
}
