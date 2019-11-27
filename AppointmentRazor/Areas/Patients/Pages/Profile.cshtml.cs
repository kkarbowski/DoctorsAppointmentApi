using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentRazor.Services.Interfaces;
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


        public void OnGet()
        {
            if(Patient == null)
            {
                Patient = patientsProfileService.GetCurrentPatient();
            }

        }
    }
}