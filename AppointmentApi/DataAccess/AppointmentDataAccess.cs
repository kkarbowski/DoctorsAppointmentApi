using AppointmentApi.Database;
using AppointmentModel.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.DataAccess
{
    public class AppointmentDataAccess : IAppointmentDataAccess
    {
        private readonly AppDbContext _appDbContext;

        public AppointmentDataAccess(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Appointment GetAppointment(int appointmentId)
        {
            return _appDbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.AppointmentReasons)
                    .ThenInclude(ar => ar.Reason)
                .Single(a => a.AppointmentId == appointmentId);
        }

        public IEnumerable<Appointment> GetAppointments()
        {
            return _appDbContext.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Include(a => a.AppointmentReasons)
                    .ThenInclude(ar => ar.Reason)
                .ToList();
        }

        public Appointment UpdateAppointment(Appointment appointment)
        {
            _appDbContext.Entry(appointment).State = EntityState.Modified;

            var reasonsCount = appointment.AppointmentReasons.Count();
            for (int i = 0; i < reasonsCount; ++i)
                _appDbContext.Entry(appointment.AppointmentReasons.First()).State = EntityState.Modified;

            _appDbContext.SaveChanges();

            return GetAppointment(appointment.AppointmentId);
        }

        public Appointment AddAppointment(Appointment appointment)
        {
            _appDbContext.Entry(appointment).State = EntityState.Added;

            var reasonsCount = appointment.AppointmentReasons.Count();
            for (int i = 0; i < reasonsCount; ++i)
                _appDbContext.Entry(appointment.AppointmentReasons.First()).State = EntityState.Added;

            _appDbContext.SaveChanges();

            return GetAppointment(appointment.AppointmentId);
        }

        public Appointment GetAppointmentForSpecificDateAndDoctor(DateTime appointmentDate, int doctorid)
        {
            return _appDbContext.Appointments
                .Where(a => a.Doctor.UserId == doctorid
                    && a.AppointmentDate < appointmentDate.AddMinutes(15)
                    && a.AppointmentDate > appointmentDate.AddMinutes(-15))
                .FirstOrDefault();
        }
    }
}
