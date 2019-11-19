using AppointmentApi.Database;
using AppointmentApi.Tools;
using AppointmentModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApi.Business
{
    public static class PatientBusiness
    {
        public static Patient[] GetPatients()
        {
            using var dbContext = new AppDbContext();

            return dbContext.Patients.ToArray();
        }

        public static Patient AddPatient(Patient patient)
        {
            using var dbContext = new AppDbContext();
            
            patient.Password = HashGenerator.GenerateHash(patient.Password);
            var newPatient = dbContext.Patients.Add(patient);
            dbContext.SaveChanges();

            return newPatient.Entity;
        }

        public static Patient GetPatient(int patientId)
        {
            using var dbContext = new AppDbContext();

            return dbContext.Patients.Find(patientId);
        }
    }
}
