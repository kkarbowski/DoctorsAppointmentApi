using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Utilities.Localization
{
    public static class CurentCultureUtils
    {
        public static Dictionary<string, string> GetCurrentCultureRouteData()
        {
            var requestCulture = CultureInfo.CurrentCulture;
            var routeData = new Dictionary<string, string>();
            routeData["culture"] = requestCulture.Name;

            return routeData;
        }
    }
}
