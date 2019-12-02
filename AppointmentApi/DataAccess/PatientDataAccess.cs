using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Database;
using AppointmentModel;
using AppointmentModel.Model;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Appointment> GetPatientAppointments(int patientId)
        {
            return _appDbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.AppointmentReasons)
                    .ThenInclude(ar => ar.Reason)
                .Where(a => a.Patient.UserId == patientId)
                .ToList();
        }
    }
}
