using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Services
{
    public static class ApiConfiguration
    {
        public static string GetBaseUrl()
        {
            string url = Environment.GetEnvironmentVariable("BACKEND_URL");
            if (url == null)
            {
                return BASE_URL;
            }
            return url;
        }

        private const string BASE_URL = "http://localhost:59147";
    }
}
