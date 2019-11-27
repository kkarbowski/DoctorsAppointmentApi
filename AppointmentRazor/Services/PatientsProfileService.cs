using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentRazor.Services.Interfaces;

namespace AppointmentRazor.Services
{
    public class PatientsProfileService : IPatientsProfileService
    {
        public Patient GetCurrentPatient()
        {
            return new Patient()
            {
                FirstName = "John",
                LastName = "Snow",
                Phone = "978587125",
                Mail = "john.snow@gmail.com",
                BirthDate = DateTime.Now
            };
        }
    }
}
