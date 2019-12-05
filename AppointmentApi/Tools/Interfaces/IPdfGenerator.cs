using AppointmentModel.Model;
using DinkToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentApi.Tools.Interfaces
{
    public interface IPdfGenerator
    {
        public HtmlToPdfDocument GenerateHtmlToPdfDocument(IEnumerable<Appointment> appointments, bool skipCanceled);
        public string GenerateHtmlForPdf();

        public GlobalSettings PrepareGlobalSettings();
        public ObjectSettings PrepareObjectSettings();
    }
}
