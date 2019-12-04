using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
    public class AppointmentsService : IAppointmentsService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentsService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> CancelAppointment(int appointmentId)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Appointment/{appointmentId}";
            _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            HttpResponseMessage response;
            try
            {
                Log.Debug($"DELETE request, URI={uri}");
                response = await _httpClient.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "DELETE appointment failed");
                return false;
            }


            if (!response.IsSuccessStatusCode)
            {
                Log.Error($"DELETE appointment failed, status code = {response.StatusCode}");
                return false;
            }

            Log.Debug("DELETE appointment request success");
            return true;
        }

        public async Task<List<Reason>> GetAllAppointmentReasons()
        {
            var uri = $"{ApiConfiguration.baseUrl}/Reason";
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
                Log.Error(ex, "GET appointment/reason failed");
                return null;
            }

            List<Reason> reasons = null;

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("GET appointment/reason request success");
                reasons = JsonUtils.Deserialize<List<Reason>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Log.Error($"GET appointment/reason failed, status code = {response.StatusCode}");
            }

            return reasons;
        }

        public async Task<List<Appointment>> GetAllAppointmentsForDoctor(int doctorId)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Doctor/{doctorId}/Appointment";
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

            List<Appointment> appointments = null;

            if (response.IsSuccessStatusCode)
            {
                appointments = JsonUtils.Deserialize<List<Appointment>>(await response.Content.ReadAsStringAsync());
            }

            return appointments;
        }

        public async Task<List<Appointment>> GetAllAppointmentsForUser(int patientId)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Patient/{patientId}/Appointment";
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
                Log.Error(ex, "GET patient/appointment failed");
                return null;
            }

            List<Appointment> appointments = null;

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("GET patient/appointment success");
                appointments = JsonUtils.Deserialize<List<Appointment>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Log.Error($"GET patient/appointment failed, status code = {response.StatusCode}");
            }

            return appointments;
        }

        public async Task<List<Doctor>> GetAllAvailableDoctors()
        {
            var uri = $"{ApiConfiguration.baseUrl}/Doctor";
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
                Log.Error(ex, "GET doctors failed");
                return null;
            }

            List<Doctor> reasons = null;

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("GET doctors succeeded");
                reasons = JsonUtils.Deserialize<List<Doctor>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                Log.Error($"GET doctors failed, status code = {response.StatusCode}");
            }

            return reasons;
        }

        public async Task<AppointmentSetResponse> SetAppointment(Appointment appointment)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Appointment";
            _httpClient.DefaultRequestHeaders.Authorization =
              new AuthenticationHeaderValue("Bearer", AuthenticationUtils.GetUserToken(_httpContextAccessor.HttpContext));

            var appointmentJson = JsonUtils.Serialize(appointment);

            HttpResponseMessage response;
            try
            {
                Log.Debug($"POST request, URI = {uri}, Content = {appointmentJson}");
                response = await _httpClient.PostAsync(uri, new StringContent(appointmentJson, Encoding.UTF8, "application/json"));
            }
            catch (Exception ex)
            {
                Log.Error(ex, "POST Appointment failed");
                //TODO Change this once server returns this option
                return AppointmentSetResponse.DATE_NOT_AVAILABLE;
            }


            if (!response.IsSuccessStatusCode)
            {
                Log.Error($"POST Appointment failed, status code = {response.StatusCode}");
                //TODO Change this once server returns this option
                return AppointmentSetResponse.DOCTOR_NOT_AVAILABLE;
            }

            Log.Debug("POST Appointment success");
            return AppointmentSetResponse.CORRECT;
        }
    }
}
