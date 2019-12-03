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
    public class DoctorDataAccess : IDoctorDataAccess
    {
        private readonly AppDbContext _appDbContext;

        public DoctorDataAccess(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Doctor UpdateDoctor(Doctor doctor)
        {
            var newDoctor = _appDbContext.Doctors.Update(doctor);
            _appDbContext.SaveChanges();

            return newDoctor.Entity;
        }

        public Doctor GetDoctor(int doctorId)
        {
            return _appDbContext.Doctors.Find(doctorId);
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return _appDbContext.Doctors.ToList();
        }

        public IEnumerable<Appointment> GetDoctorAppointments(int doctorId)
        {
            return _appDbContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.AppointmentReasons)
                    .ThenInclude(ar => ar.Reason)
                .Where(a => a.Doctor.UserId == doctorId)
                .ToList();
        }
    }
}
