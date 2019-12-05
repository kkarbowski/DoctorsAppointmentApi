using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
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

        public bool IsShownForDoctor { get; set; } = false;

        public async Task OnGetAsync()
        {
            await GetAppointments();
        }

        public async Task OnGetShowOnlyActiveAsync()
        {
            await GetAppointments();
            if(Appointments != null)
            {
                Appointments = Appointments.Where(apt => apt.AppointmentDate > DateTime.UtcNow && !apt.IsCanceled).ToList();
            }
        }

        public async Task OnGetShowAllAsync()
        {
            await GetAppointments();
        }

        public async Task OnGetCancelAppointmentAsync(int id)
        {
            await appointmentsService.CancelAppointment(id);
            await GetAppointments();
        }

        private async Task GetAppointments()
        {
            if (Appointments == null)
            {
                var patientId = AuthenticationUtils.GetPatientId(HttpContext);
                if(patientId.HasValue)
                {
                    if(AuthenticationUtils.IsUserInRole(HttpContext, Role.Doctor)) {
                        IsShownForDoctor = true;
                        Appointments = await appointmentsService.GetAllAppointmentsForDoctor(patientId.Value);
                    } else
                    {
                        IsShownForDoctor = false;
                        Appointments = await appointmentsService.GetAllAppointmentsForUser(patientId.Value);
                    }

                    if(Appointments != null)
                    {
                        var futureAppointments = Appointments.Where(apt => apt.AppointmentDate >= DateTime.UtcNow).ToList();
                        var pastAppointments = Appointments.Where(apt => apt.AppointmentDate < DateTime.UtcNow).ToList();
                        futureAppointments = futureAppointments.OrderBy(apt => apt.AppointmentDate).ToList();
                        pastAppointments = pastAppointments.OrderByDescending(apt => apt.AppointmentDate).ToList();
                        Appointments = futureAppointments.Concat(pastAppointments).ToList();

                        LocalizedReasons = new Dictionary<int, string>();

                        Appointments.ForEach(appointment =>
                        {
                            LocalizedReasons.Add(appointment.AppointmentId, GetLozalizedReasons(appointment));
                        });
                    }
                }
            }
        }

        private string GetLozalizedReasons(Appointment appointment)
        {
            string currentCulture = CurentCultureUtils.GetCurrentCulture();

            return string.Join(", ", 
                appointment.AppointmentReasons.Where(appToReas => appToReas.Reason != null && appToReas.Reason.LangReasonDictionary != null)
                                              .Select(appToReas => appToReas.Reason.LangReasonDictionary[currentCulture]).ToList());
        }
    }
}