using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Json;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace AppointmentRazor.Services
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
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
                Log.Debug($"GET request, URI = {uri}");
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GET Patient failed");
                return null;
            }

            List<Patient> patients = null;

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("GET Patients success");
                patients = JsonUtils.Deserialize<List<Patient>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Log.Error($"GET Patients failed, status code = {response.StatusCode}");
            }

            return patients;
        }

        public async Task<Doctor> GetDoctor(int doctorId)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Doctor/{doctorId}";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            HttpResponseMessage response;
            try
            {
                Log.Debug($"GET request, URI = {uri}");
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GET Doctor failed");
                return null;
            }

            Doctor doctor = null;

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("GET Doctor success");
                doctor = JsonUtils.Deserialize<Doctor>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Log.Error($"GET Doctor failed, status code = {response.StatusCode}");
            }

            return doctor;
        }

        public async Task<Patient> GetPatient(int patientId)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Patient/{patientId}";
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            HttpResponseMessage response;
            try
            {
                Log.Debug($"GET request, URI = {uri}");
                response = await _httpClient.GetAsync(uri);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GET Patient failed");
                return null;
            }

            Patient patient = null;

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("GET Patient succeeded");
                patient = JsonUtils.Deserialize<Patient>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Log.Error($"GET Patient failed, status code = {response.StatusCode}");
            }

            return patient;
        }
    }
}
