using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Json;
using Microsoft.AspNetCore.Http;

namespace AppointmentRazor.Services
{
    public class PatientsProfileService : IPatientsProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientsProfileService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            var uri = $"{ApiConfiguration.baseUrl}/Patient";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception)
            {
                return null;
            }

            List<Patient> patients = null;

            if (response.IsSuccessStatusCode)
            {
                patients = JsonUtils.Deserialize<List<Patient>>(await response.Content.ReadAsStringAsync());
            }

            return patients;
        }

        public async Task<Patient> GetPatient(int patientId)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Patient/{patientId}";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception)
            {
                return null;
            }

            Patient patient = null;

            if (response.IsSuccessStatusCode)
            {
                patient = JsonUtils.Deserialize<Patient>(await response.Content.ReadAsStringAsync());
            }

            return patient;
        }
    }
}
