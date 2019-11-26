using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Utilities.Localization
{
    public static class CurentCultureUtils
    {
        public static string DefaultCulture = "en";

        public static string[] SupportedCultures = { "en", "pl" };

        public static Dictionary<string, string> GetCurrentCultureRouteData()
        {
            var requestCulture = CultureInfo.CurrentCulture;
            var routeData = new Dictionary<string, string>();
            routeData["culture"] = requestCulture.Name;

            return routeData;
        }

        public static string GetCurrentCultureLink(string route)
        {
            var requestCulture = CultureInfo.CurrentCulture.Name;

            return $"/{requestCulture}/{route}";
        }

        public static string GetCurrentCulture()
        {
            return CultureInfo.CurrentCulture.Name;
        }
    }
}
