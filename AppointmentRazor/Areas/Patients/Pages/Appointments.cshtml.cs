using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Pages
{
    public class AppointmentsModel : PageModel
    {
        private IAppointmentsService appointmentsService;

        public AppointmentsModel(IAppointmentsService appointmentsService) : base()
        {
            this.appointmentsService = appointmentsService;
        }

        public List<Appointment> Appointments { get; set; }
        public Dictionary<int, string> LocalizedReasons { get; set; }

        public void OnGet()
        {
            GetAppointments();
        }

        public void OnGetShowOnlyActive()
        {
            GetAppointments();
            if(Appointments != null)
            {
                Appointments = Appointments.Where(apt => apt.AppointmentDate > DateTime.UtcNow).ToList();
            }
        }

        public void OnGetShowAll()
        {
            GetAppointments();
        }

        public void OnGetCancelAppointment(int appointmentId)
        {
            appointmentsService.CancelAppointment(appointmentId);
            GetAppointments();
        }

        private void GetAppointments()
        {
            if (Appointments == null)
            {
                Appointments = appointmentsService.GetAllAppointmentsForCurrentUser();
                Appointments.OrderBy(apt => apt.AppointmentDate).ToList();

                LocalizedReasons = new Dictionary<int, string>();

                Appointments.ForEach(appointment =>
                {
                    LocalizedReasons.Add(appointment.Id, GetLozalizedReasons(appointment));
                });
            }
        }

        private string GetLozalizedReasons(Appointment appointment)
        {
            string currentCulture = CurentCultureUtils.GetCurrentCulture();

            return string.Join(", ", appointment.Reasons.Select(reason => reason[currentCulture]).ToList());
        }
    }
}