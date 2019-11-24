using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Database;
using AppointmentApi.Tools;
using AppointmentApi.Tools.Interfaces;
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
    public class PatientBusiness : IPatientBusiness
    {
        private readonly IHashGenerator _hashGenerator;
        private readonly IPatientDataAccess _patientDataAccess;

        public PatientBusiness(IHashGenerator hashGenerator, IPatientDataAccess patientDataAccess)
        {
            _hashGenerator = hashGenerator;
            _patientDataAccess = patientDataAccess;
        }

        public Patient[] GetPatients()
        {
            return _patientDataAccess.GetPatients();
        }

        public Patient AddPatient(Patient patient)
        {
            try
            {
                patient.Password = _hashGenerator.GenerateHash(patient.Password);
                return _patientDataAccess.AddPatient(patient);
            }
            catch
            {
                return null;
            }
        }

        public Patient GetPatient(int patientId)
        {
            return _patientDataAccess.GetPatient(patientId);
        }
    }
}
