using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Json;
using Microsoft.AspNetCore.Http;

namespace AppointmentRazor.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentsService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CancelAppointment(int appointmentId)
        {
            //TODO: Implement me
            return true;
        }

        public async Task<List<Reason>> GetAllAppointmentReasons()
        {
            var uri = $"{ApiConfiguration.baseUrl}/Reason";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception)
            {
                return null;
            }

            List<Reason> reasons = null;

            if (response.IsSuccessStatusCode)
            {
                reasons = JsonUtils.Deserialize<List<Reason>>(await response.Content.ReadAsStringAsync());
            }

            return reasons;
        }

        public async Task<List<Appointment>> GetAllAppointmentsForUser(int patientId)
        {
            //TODO: Uncomment this once API return proper response
            //var uri = $"{ApiConfiguration.baseUrl}/Patient/{patientId}/Appointment";
            //_httpClient.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            //HttpResponseMessage response;
            //try
            //{
            //    response = await _httpClient.GetAsync(uri);
            //}
            //catch (Exception)
            //{
            //    return null;
            //}

            //List<Appointment> appointments = null;

            //if (response.IsSuccessStatusCode)
            //{
            //    appointments = JsonUtils.Deserialize<List<Appointment>>(await response.Content.ReadAsStringAsync());
            //}

            //return appointments;
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

        public async Task<List<Doctor>> GetAllAvailableDoctors()
        {
            var uri = $"{ApiConfiguration.baseUrl}/Doctor";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception)
            {
                return null;
            }

            List<Doctor> reasons = null;

            if (response.IsSuccessStatusCode)
            {
                reasons = JsonUtils.Deserialize<List<Doctor>>(await response.Content.ReadAsStringAsync());
            }

            return reasons;
        }

        public async Task<AppointmentSetResponse> SetAppointment(Appointment appointment)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Appointment";
            _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            var appointmentJson = JsonUtils.Serialize(appointment);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(uri, new StringContent(appointmentJson, Encoding.UTF8, "application/json"));
            }
            catch (Exception)
            {
                //TODO Change this once server returns this option
                return AppointmentSetResponse.DATE_NOT_AVAILABLE;
            }


            if (!response.IsSuccessStatusCode)
            {
                //TODO Change this once server returns this option
                return AppointmentSetResponse.DOCTOR_NOT_AVAILABLE;
            }

            return AppointmentSetResponse.CORRECT;
        }
    }
}
