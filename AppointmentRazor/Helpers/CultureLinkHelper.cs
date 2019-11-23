using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Helpers
{
    public static class CultureLinkHelper
    {
        public static string CultureLink(this IHtmlHelper<dynamic> html, String routeDestination, String routeName)
        {
            var requestCulture = CultureInfo.CurrentCulture;
            var routeData = new Dictionary<string, string>
            {
                ["culture"] = requestCulture.Name
            };

            return $"<a class='nav-link' asp-all-route-data='{routeData}' asp-area='' asp-page='{routeDestination}'>{routeName}</a>";
        }
    }
}
