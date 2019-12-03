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

        IProfileService patientsProfileService;

        public PatientsModel(IProfileService patientsProfileService)
        {
            this.patientsProfileService = patientsProfileService;
        }

        public List<Patient> Patients;

        public bool Filter { get; set; }

        public async Task OnGetAsync(string filterQuery)
        {
            if (Patients == null)
            {
                Patients = await patientsProfileService.GetAllPatients();
                if(Patients != null && filterQuery != null)
                {
                    Patients = Patients.Where(patient => {
                        if(patient.FullName == null)
                        {
                            return false;
                        }
                        return patient.FullName.Contains(filterQuery);
                    }).ToList();

                    Filter = true;
                }
            }
        }
    }
}
