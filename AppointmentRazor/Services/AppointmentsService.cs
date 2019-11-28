using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;

namespace AppointmentRazor.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        public List<Dictionary<string, string>> GetAllAppointmentReasons()
        {
            return new List<Dictionary<string, string>>
            {
                new Dictionary<string,string>()
                {
                    {"pl","Kaszel"},
                    {"en","Coughing"},
                },
                new Dictionary<string,string>()
                {
                    {"pl","Ból gardła"},
                    {"en","Sore throat"},
                },
                new Dictionary<string,string>()
                {
                    {"pl","Katar"},
                    {"en","Runny nose"},
                },
                new Dictionary<string,string>()
                {
                    {"pl","Inne"},
                    {"en","Other"},
                },
            };
        }

        public List<Appointment> GetAllAppointmentsForCurrentUser()
        {
            throw new NotImplementedException();
        }

        public List<Appointment> GetAllAppointmentsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public List<Doctor> GetAllAvailableDoctors()
        {
            return new List<Doctor>()
            {
               new Doctor(){ UserId = 0, FullName = "Edward Snowden"},
               new Doctor(){ UserId = 0, FullName = "Samantha Becker"},
            };
        }

        public AppointmentSetResponse SetAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
