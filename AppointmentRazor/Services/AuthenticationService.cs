using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppointmentModel;
using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Json;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace AppointmentRazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AuthenticationReponse> Login(User user)
        {
            var uri = $"{ApiConfiguration.GetBaseUrl()}/User/login";

            var userJson = JsonUtils.Serialize(user);

            HttpResponseMessage response;
            try
            {
                Log.Debug($"POST request, URI = {uri}");
                response = await _httpClient.PostAsync(uri, new StringContent(userJson, Encoding.UTF8, "application/json"));
            } catch (Exception ex)
            {
                Log.Error(ex, "POST User/login failed");
                return new AuthenticationReponse() { WasAuthenticationCorrect = false };
            }

            Token token = null;
            var authenticationResponse = new AuthenticationReponse()
            {
                WasAuthenticationCorrect = response.IsSuccessStatusCode
            };

            if (response.IsSuccessStatusCode)
            {
                Log.Debug("POST User/login success");
                token = JsonUtils.Deserialize<Token>(await response.Content.ReadAsStringAsync());

                authenticationResponse.Roles = token?.Roles.ToList();
                authenticationResponse.PatientId = token?.UserId;
                authenticationResponse.Token = token?.TokenString;

                AuthenticationUtils.SaveUserToSession(_httpContextAccessor.HttpContext, authenticationResponse);    
            }
            else
            {
                Log.Error($"POST User/login failed, status code = {response.StatusCode}");
            }

            return authenticationResponse;
        }

        public async Task<bool> Register(Patient patient)
        {
            var uri = $"{ApiConfiguration.GetBaseUrl()}/Patient";

            var userJson = JsonUtils.Serialize(patient);

            HttpResponseMessage response;
            try
            {
                Log.Debug($"POST request, URI = {uri}");
                response = await _httpClient.PostAsync(uri, new StringContent(userJson, Encoding.UTF8, "application/json"));
            } catch (Exception ex)
            {
                Log.Error(ex, "POST Patient failed");
                return false;
            }

            var success = response.IsSuccessStatusCode;

            if (success)
            {
                Log.Debug("POST Patient succeeded");
            }
            else
            {
                Log.Error($"POST Patient failed, status code = {response.StatusCode}");
            }

            return success;
        }
    }
}
