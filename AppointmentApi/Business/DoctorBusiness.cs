using AppointmentApi.DataAccess;
using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Database;
using AppointmentApi.Tools;
using AppointmentApi.Tools.Interfaces;
using AppointmentModel;
using AppointmentModel.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApi.Business
{
    public class DoctorBusiness : IDoctorBusiness
    {
        private readonly IHashGenerator _hashGenerator;
        private readonly IDoctorDataAccess _doctorDataAccess;

        public DoctorBusiness(IHashGenerator hashGenerator, IDoctorDataAccess doctorDataAccess)
        {
            _hashGenerator = hashGenerator;
            _doctorDataAccess = doctorDataAccess;
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _doctorDataAccess.GetDoctors().Select(p => (Doctor)p.NoPassword()).ToList();
        }

        public Doctor AddDoctor(Doctor doctor)
        {
            return UpdateDoctor((Doctor)doctor.NoUserId());
        }

        public Doctor GetDoctor(int doctorId)
        {
            return (Doctor)_doctorDataAccess.GetDoctor(doctorId).NoPassword();
        }

        public Doctor UpdateDoctor(Doctor doctor)
        {
            try
            {
                doctor.Password = _hashGenerator.GenerateHash(doctor.Password);
                doctor.Roles = new List<string> { Role.Doctor, Role.Patient };
                return _doctorDataAccess.UpdateDoctor((Doctor)doctor.NoDateTimeAdd());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Encountered an exception while executing DoctorBusiness.UpdateDoctor");
                return null;
            }
        }
    }
}
