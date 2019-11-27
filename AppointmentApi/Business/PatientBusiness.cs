using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Database;
using AppointmentApi.Tools;
using AppointmentApi.Tools.Interfaces;
using AppointmentModel;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            return _patientDataAccess.GetPatients().Select(p => (Patient)p.NoPassword()).ToArray();
        }

        public Patient AddPatient(Patient patient)
        {
            try
            {
                patient.Password = _hashGenerator.GenerateHash(patient.Password);
                patient.Roles = new List<string> { Role.Patient };
                return _patientDataAccess.AddPatient(patient);
            }
            catch
            {
                return null;
            }
        }

        public Patient GetPatient(int patientId)
        {
            return (Patient)_patientDataAccess.GetPatient(patientId).NoPassword();
        }

        public Patient UpdatePatient(Patient patient)
        {
            return AddPatient(patient);
        }
    }
}
