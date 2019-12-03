using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Areas.Doctors.Pages
{
    public class ProfileModel : PageModel
    {
        IProfileService patientsProfileService;

        public ProfileModel(IProfileService patientsProfileService)
        {
            this.patientsProfileService = patientsProfileService;
        }


        public Doctor Doctor;


        public async Task OnGetAsync()
        {
            if (Doctor == null)
            {
                var doctorId = AuthenticationUtils.GetPatientId(HttpContext);
                if (doctorId.HasValue)
                {
                    Doctor = await patientsProfileService.GetDoctor(doctorId.Value);
                }
            }

        }
    }
}
