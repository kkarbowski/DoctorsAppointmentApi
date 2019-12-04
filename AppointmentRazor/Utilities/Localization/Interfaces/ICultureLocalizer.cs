using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentRazor.Utilities.Localization.Interfaces
{
    public interface ICultureLocalizer
    {
        public LocalizedString Text(string key, params string[] arguments);
    }
}
