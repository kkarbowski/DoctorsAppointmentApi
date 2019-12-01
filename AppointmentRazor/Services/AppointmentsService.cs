using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;

namespace AppointmentRazor.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly HttpClient _httpClient;

        public AppointmentsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CancelAppointment(int appointmentId)
        {
            //TODO: Implement me
            return true;
        }

        public async Task<List<Appointment2Reason>> GetAllAppointmentReasons()
        {
            return new List<Appointment2Reason>
            {
                new Appointment2Reason()
                {
                    Reason = new Reason()
                    {
                        ReasonId = 0,
                        LangReasonDictionary =  new Dictionary<string,string>()
                        {
                            {"pl","Kaszel"},
                            {"en","Coughing"},
                        }
                    }
                },
                new Appointment2Reason()
                {
                    Reason = new Reason()
                    {
                        ReasonId = 1,
                        LangReasonDictionary =   new Dictionary<string,string>()
                        {
                            {"pl","Ból gardła"},
                            {"en","Sore throat"},
                        }
                    }
                },
                new Appointment2Reason()
                {
                    Reason = new Reason()
                    {
                        ReasonId = 2,
                        LangReasonDictionary =  new Dictionary<string,string>()
                        {
                            {"pl","Katar"},
                            {"en","Runny nose"},
                        }
                    }
                },
                new Appointment2Reason()
                {
                    Reason = new Reason()
                    {
                        ReasonId = 3,
                        LangReasonDictionary =  new Dictionary<string,string>()
                        {
                            {"pl","Inne"},
                            {"en","Other"},
                        }
                    }
                }
            };
        }

        public async Task<List<Appointment>> GetAllAppointmentsForCurrentUser()
        {
            return new List<Appointment>
            {
                new Appointment()
                {
                    AppointmentId = 1,
                    AppointmentReasons = new List<Appointment2Reason>
                    {
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary = new Dictionary<string,string>()
                                {
                                    {"pl","Kaszel"},
                                    {"en","Coughing"},
                                }
                            }
                        },
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary = new Dictionary<string,string>()
                                {
                                    {"pl","Ból gardła"},
                                    {"en","Sore throat"},
                                }
                            }
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
                    AppointmentId = 2,
                    AppointmentReasons = new List<Appointment2Reason>
                    {
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary = new Dictionary<string,string>()
                                {
                                    {"pl","Katar"},
                                    {"en","Runny nose"},
                                }
                            }
                        },
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary =  new Dictionary<string,string>()
                                {
                                    {"pl","Inne"},
                                    {"en","Other"},
                                }
                            }
                        }
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
                    AppointmentId = 3,
                   AppointmentReasons = new List<Appointment2Reason>
                    {
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary = new Dictionary<string,string>()
                                {
                                    {"pl","Katar"},
                                    {"en","Runny nose"},
                                }
                            }
                        },
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary =  new Dictionary<string,string>()
                                {
                                    {"pl","Inne"},
                                    {"en","Other"},
                                }
                            }
                        }
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
                    AppointmentId = 4,
                    AppointmentReasons = new List<Appointment2Reason>
                    {
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary = new Dictionary<string,string>()
                                {
                                    {"pl","Kaszel"},
                                    {"en","Coughing"},
                                }
                            }
                        },
                        new Appointment2Reason()
                        {
                            Reason = new Reason
                            {
                                LangReasonDictionary = new Dictionary<string,string>()
                                {
                                    {"pl","Ból gardła"},
                                    {"en","Sore throat"},
                                }
                            }
                        }
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

        public async Task<List<Appointment>> GetAllAppointmentsForUser(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Doctor>> GetAllAvailableDoctors()
        {
            return new List<Doctor>()
            {
               new Doctor(){ UserId = 0, FullName = "Edward Snowden"},
               new Doctor(){ UserId = 1, FullName = "Samantha Becker"},
            };
        }

        public async Task<AppointmentSetResponse> SetAppointment(Appointment appointment)
        {
            return AppointmentSetResponse.CORRECT;
        }
    }
}
