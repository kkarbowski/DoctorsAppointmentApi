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
using AppointmentRazor.Utilities.Json;

namespace AppointmentRazor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;

        public AuthenticationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthenticationReponse> Login(User user)
        {
            var uri = $"{ApiConfiguration.baseUrl}/User/login";

            var userJson = JsonUtils.Serialize(user);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(uri, new StringContent(userJson, Encoding.UTF8, "application/json"));
            } catch (Exception)
            {
                return new AuthenticationReponse() { WasAuthenticationCorrect = false };
            }

            Token token = null;
  
            if (response.IsSuccessStatusCode)
            {
                token = JsonUtils.Deserialize<Token>(await response.Content.ReadAsStringAsync());
            }

            return new AuthenticationReponse()
            {
                WasAuthenticationCorrect = response.IsSuccessStatusCode,
                Roles = token?.Roles.ToList(),
                PatientId = token?.UserId,
                Token = token?.TokenString
            };
        }

        public async Task<bool> Register(Patient patient)
        {
            var uri = $"{ApiConfiguration.baseUrl}/Patient";

            var userJson = JsonUtils.Serialize(patient);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(uri, new StringContent(userJson, Encoding.UTF8, "application/json"));
            } catch (Exception)
            {
                return false;
            }

            return response.IsSuccessStatusCode;
        }
    }
}
