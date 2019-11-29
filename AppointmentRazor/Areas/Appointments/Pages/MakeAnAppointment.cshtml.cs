using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentRazor.Areas.Patients.Pages
{
    public class MakeAnAppointmentModel : PageModel
    {

        private IAppointmentsService appointmentsService;

        public MakeAnAppointmentModel(IAppointmentsService appointmentsService) :base()
        {
            this.appointmentsService = appointmentsService;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public SelectList AvailableDoctors { get; set; }

        public int SelectedDoctor { get; set; }

        public SelectList AvailableReasons { get; set; }

        public int[] SelectedResons { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public void OnGet()
        {
            if(AvailableDoctors == null)
            {
                var availableDoctors = appointmentsService.GetAllAvailableDoctors();
                AvailableDoctors = new SelectList(availableDoctors, nameof(Doctor.UserId),nameof(Doctor.FullName));
            }
            if(SelectedResons == null)
            {
                var curentCulture = CurentCultureUtils.GetCurrentCulture();
                var resons = appointmentsService.GetAllAppointmentReasons().Select(resonDict => resonDict[curentCulture]);
                AvailableReasons = new SelectList(resons);
            }
        }

     

        public IActionResult OnPost()
        {
            if(true)
            {
                HttpContext.Response.Redirect(CurentCultureUtils.GetCurrentCultureLink("Appointments/AppointmentMade"));

                return null;
            } else
            {
                return Page();
            }
   
        }
    }
}
