using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Pages
{
    public class ProfileModel : PageModel
    {
        IPatientsProfileService patientsProfileService;

        public ProfileModel(IPatientsProfileService patientsProfileService) : base()
        {
            this.patientsProfileService = patientsProfileService;
        }


        public Patient Patient;

        public string Msg { get; set; }


        public async Task OnGetAsync()
        {
            if(Patient == null)
            {
                var patientId = AuthenticationUtils.GetPatientId(HttpContext);
                if(patientId.HasValue)
                {
                    Patient = await patientsProfileService.GetCurrentPatient(patientId.Value);
                }
            }

        }
    }
}