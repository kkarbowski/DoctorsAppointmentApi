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

        public AppointmentsModel(IAppointmentsService appointmentsService)
        {
            this.appointmentsService = appointmentsService;
        }

        public List<Appointment> Appointments { get; set; }
        public Dictionary<int, string> LocalizedReasons { get; set; }

        public async Task OnGetAsync()
        {
            await GetAppointments();
        }

        public async Task OnGetShowOnlyActiveAsync()
        {
            await GetAppointments();
            if(Appointments != null)
            {
                Appointments = Appointments.Where(apt => apt.AppointmentDate > DateTime.UtcNow).ToList();
            }
        }

        public async Task OnGetShowAllAsync()
        {
            await GetAppointments();
        }

        public async Task OnGetCancelAppointmentAsync(int appointmentId)
        {
            await appointmentsService.CancelAppointment(appointmentId);
            await GetAppointments();
        }

        private async Task GetAppointments()
        {
            if (Appointments == null)
            {
                Appointments = await appointmentsService.GetAllAppointmentsForCurrentUser();
                Appointments.OrderBy(apt => apt.AppointmentDate).ToList();

                LocalizedReasons = new Dictionary<int, string>();

                Appointments.ForEach(appointment =>
                {
                    LocalizedReasons.Add(appointment.AppointmentId, GetLozalizedReasons(appointment));
                });
            }
        }

        private string GetLozalizedReasons(Appointment appointment)
        {
            string currentCulture = CurentCultureUtils.GetCurrentCulture();

            return string.Join(", ", 
                appointment.AppointmentReasons.Select(appToReas => appToReas.Reason.LangReasonDictionary[currentCulture]).ToList());
        }
    }
}