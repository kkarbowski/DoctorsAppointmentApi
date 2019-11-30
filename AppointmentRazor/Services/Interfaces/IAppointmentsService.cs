using AppointmentModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services.Interfaces
{
    public interface IAppointmentsService
    {
        /// <summary>
        /// Fetches list of all appointments for currently logged user
        /// </summary>
        /// <returns>A list of all appointmes for currently logged user or null if no user is logged</returns>
        public List<Appointment> GetAllAppointmentsForCurrentUser();

        /// <summary>
        /// Fetches appointment list for given user
        /// </summary>
        /// <param name="userId">id of a user that we want to fetch appointments for</param>
        /// <returns>A list of appointments</returns>
        public List<Appointment> GetAllAppointmentsForUser(string userId);

        /// <summary>
        /// Tries to set an appointment
        /// </summary>
        /// <param name="appointment">An appointment to be set</param>
        /// <returns>an appontment set response</returns>
        public AppointmentSetResponse SetAppointment(Appointment appointment);

        /// <summary>
        /// Fetches all available doctors
        /// </summary>
        /// <returns>a list of available doctors that can be picked for an appointment</returns>
        public List<Doctor> GetAllAvailableDoctors();

        /// <summary>
        /// Fetches all appointments reasons
        /// </summary>
        /// <returns>A list of appointments reasons, like a sore throat</returns>
        public List<Dictionary<string, string>> GetAllAppointmentReasons();

        /// <summary>
        /// This function tries to cancel a given appointment
        /// </summary>
        /// <param name="appointmentId">id of an appointment we want to cancel</param>
        /// <returns>whenever opperation was successuf</returns>
        public bool CancelAppointment(int appointmentId);
    }
}
