using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services.Interfaces
{
    public interface AppointmentsService
    {
        public List<Appointment> GetAllAppointmentsForCurrentUser();
        public List<Appointment> GetAllAppointmentsForUser(string userId);
    }
}
