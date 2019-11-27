using AppointmentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services.Interfaces
{
    public interface IPatientsProfileService
    {
        public Patient GetCurrentPatient();
    }
}
