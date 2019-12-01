using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentRazor.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppointmentRazor.Areas.Doctors.Pages
{
    public class PatientsModel : PageModel
    {

        IPatientsProfileService patientsProfileService;

        public PatientsModel(IPatientsProfileService patientsProfileService)
        {
            this.patientsProfileService = patientsProfileService;
        }

        public List<Patient> Patients;

        public async Task OnGetAsync()
        {
            if (Patients == null)
            {
                Patients = await patientsProfileService.GetAllPatients();   
            }
        }
    }
}
