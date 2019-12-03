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
        /// Fetches appointment list for given user
        /// </summary>
        /// <param name="patientId">id of a user that we want to fetch appointments for</param>
        /// <returns>A list of appointments</returns>
        public Task<List<Appointment>> GetAllAppointmentsForUser(int patientId);

        /// <summary>
        /// Fetches appointment list for given doctor
        /// </summary>
        /// <param name="doctorId">id of a doctor that we want to fetch appointments for</param>
        /// <returns>A list of appointments</returns>
        public Task<List<Appointment>> GetAllAppointmentsForDoctor(int doctorId);

        /// <summary>
        /// Tries to set an appointment
        /// </summary>
        /// <param name="appointment">An appointment to be set</param>
        /// <returns>an appontment set response</returns>
        public Task<AppointmentSetResponse> SetAppointment(Appointment appointment);

        /// <summary>
        /// Fetches all available doctors
        /// </summary>
        /// <returns>a list of available doctors that can be picked for an appointment</returns>
        public Task<List<Doctor>> GetAllAvailableDoctors();

        /// <summary>
        /// Fetches all appointments reasons
        /// </summary>
        /// <returns>A list of appointments reasons, like a sore throat</returns>
        public Task<List<Reason>> GetAllAppointmentReasons();

        /// <summary>
        /// This function tries to cancel a given appointment
        /// </summary>
        /// <param name="appointmentId">id of an appointment we want to cancel</param>
        /// <returns>whenever opperation was successuf</returns>
        public Task<bool> CancelAppointment(int appointmentId);
    }
}
