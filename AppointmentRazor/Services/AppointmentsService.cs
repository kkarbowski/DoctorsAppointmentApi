using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;

namespace AppointmentRazor.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        public bool CancelAppointment(int appointmentId)
        {
            //TODO: Implement me
            return true;
        }

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
            return new List<Appointment>
            {
                new Appointment()
                {
                    Id = 1,
                    Reasons = new List<Dictionary<string, string>>
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
                        }
                    },
                    AppointmentDate = new DateTime(2019, 5, 10),
                    Description = "This is some random description",
                    Doctor = new Doctor
                    {
                        FullName = "Billy Jones",
                        UserId = 3
                    },
                    Patient = new Patient()
                    {
                        FirstName = "Elisa",
                        LastName = "Johnson"
                    }
                },
                new Appointment()
                {
                    Id = 2,
                    Reasons = new List<Dictionary<string, string>>
                    {
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
                    },
                    AppointmentDate = new DateTime(2019, 8, 24),
                    Description = "To jest przykładowy opis",
                    Doctor = new Doctor
                    {
                        FullName = "Elisabeth Herey",
                        UserId = 4
                    },
                    Patient = new Patient()
                    {
                        FirstName = "George",
                        LastName = "Washington"
                    }
                },
                new Appointment()
                {
                    Id = 3,
                    Reasons = new List<Dictionary<string, string>>
                    {
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
                    },
                    AppointmentDate = new DateTime(2020, 8, 24),
                    Description = "To jest przykładowy opis",
                    Doctor = new Doctor
                    {
                        FullName = "Elisabeth Herey",
                        UserId = 4
                    },
                    Patient = new Patient()
                    {
                        FirstName = "George",
                        LastName = "Washington"
                    }
                },
                new Appointment()
                {
                    Id = 4,
                    Reasons = new List<Dictionary<string, string>>
                    {
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
                    },
                    AppointmentDate = new DateTime(2020, 10, 14),
                    Description = "To jest przykładowy opis",
                    Doctor = new Doctor
                    {
                        FullName = "Elisabeth Herey",
                        UserId = 4
                    },
                    Patient = new Patient()
                    {
                        FirstName = "George",
                        LastName = "Washington"
                    }
                }
            };
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
               new Doctor(){ UserId = 1, FullName = "Samantha Becker"},
            };
        }

        public AppointmentSetResponse SetAppointment(Appointment appointment)
        {
            return AppointmentSetResponse.CORRECT;
        }
    }
}
