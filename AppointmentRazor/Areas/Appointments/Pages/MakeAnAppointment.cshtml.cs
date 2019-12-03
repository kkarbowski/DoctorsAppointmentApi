using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppointmentRazor.Areas.Patients.Pages
{
    public class MakeAnAppointmentModel : PageModel
    {
        private readonly CultureLocalizer _cultureLocalizer;
        private IAppointmentsService _appointmentsService;

        public MakeAnAppointmentModel([FromServices] CultureLocalizer cultureLocalizer, IAppointmentsService appointmentsService)
        {
            _cultureLocalizer = cultureLocalizer;
            _appointmentsService = appointmentsService;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public SelectList AvailableDoctors { get; set; }

        public int SelectedDoctor { get; set; }

        public SelectList AvailableReasons { get; set; }

        public int[] SelectedResons { get; set; }

        [TempData]
        public string Description { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public AppointmentSetResponse appointmentsSetResponse { get; set; }

        public async Task OnGetAsync()
        {
            if(AvailableDoctors == null)
            {
                var availableDoctors = await _appointmentsService.GetAllAvailableDoctors();
                AvailableDoctors = new SelectList(availableDoctors, nameof(Doctor.UserId),nameof(Doctor.FullName));
            }
            if(AvailableReasons == null)
            {
                var curentCulture = CurentCultureUtils.GetCurrentCulture();
                var reasonFromApi = await _appointmentsService.GetAllAppointmentReasons();
                var reasons = reasonFromApi.Select(reason => 
                                        new 
                                        { 
                                            Id = reason.ReasonId, 
                                            Value = reason.LangReasonDictionary[curentCulture] 
                                        })
                                       .ToList();
                AvailableReasons = new SelectList(reasons, "Id", "Value");
            }

        }
     

        public async Task<IActionResult> OnPostAsync(int selectedDoctorId, int[] selectedReasonsIds, DateTime pickedDate)
        {
            if(ModelState.IsValid)
            {
                if(pickedDate < DateTime.UtcNow)
                {
                    ModelState.AddModelError("Date", _cultureLocalizer.Text("Date must be from the future"));
                } else
                {
                    Appointment.Doctor = new Doctor() { UserId = selectedDoctorId };
                    if (selectedReasonsIds.Length > 0)
                    {
                        Appointment.AppointmentReasons = new List<Appointment2Reason>();
                        Array.ForEach(selectedReasonsIds, (reasonId) =>
                        {
                            Appointment.AppointmentReasons.Add(new Appointment2Reason() { ReasonId = reasonId });
                        });
                    }
                    var patientId = AuthenticationUtils.GetPatientId(HttpContext);
                    if (patientId.HasValue)
                    {
                        Appointment.Patient = new Patient() { UserId = patientId.Value };
                    }


                    Appointment.AppointmentDate = pickedDate;
                    appointmentsSetResponse = await _appointmentsService.SetAppointment(Appointment);
                    if (appointmentsSetResponse == AppointmentSetResponse.CORRECT)
                    {
                        HttpContext.Response.Redirect(CurentCultureUtils.GetCurrentCultureLink("Appointments/AppointmentMade"));

                        return null;
                    }
                }
            }
            

            await OnGetAsync();
            SelectedDoctor = selectedDoctorId;
            SelectedResons = selectedReasonsIds;
            Date = pickedDate;
            return Page();
        }
    }
}
