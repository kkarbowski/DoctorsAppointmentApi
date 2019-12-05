using AppointmentApi.Tools.Interfaces;
using AppointmentModel.Model;
using DinkToPdf;
using DinkToPdf.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentApi.Tools
{
    public class PdfGenerator : IPdfGenerator
    {
        private IEnumerable<Appointment> _appointments;
        private bool _skipCanceled;

        public HtmlToPdfDocument GenerateHtmlToPdfDocument(IEnumerable<Appointment> appointments,
            bool skipCanceled)
        {
            _appointments = appointments;
            _skipCanceled = skipCanceled;
            var globalSettings = PrepareGlobalSettings();
            var objectSettings = PrepareObjectSettings();

            HtmlToPdfDocument pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return pdf;
        }

        public GlobalSettings PrepareGlobalSettings()
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report"
            };

            return globalSettings;
        }
        public ObjectSettings PrepareObjectSettings()
        {
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = GenerateHtmlForPdf(),
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            return objectSettings;
        }

        public string GenerateHtmlForPdf()
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                                <style>
                                    .header {
                                        text-align: center;
                                        color: green;
                                        padding-bottom: 35px;
                                    }

                                    table {
                                        width: 80%;
                                        border: 1px solid black;
                                        border-collapse: collapse;
                                    }

                                    td, th {
                                        border: 1px solid gray;
                                        padding: 15px;
                                        font-size: 22px;
                                        text-align: center;
                                    }

                                    table th {
                                        background-color: green;
                                        color: white;
                                    }

                                </style>
                            </head>
                            <body>
                                <div class='header'><h1>Doctor report</h1></div>
                                <table align='center'>");
            foreach (var appointment in _appointments)
            {
                if (!_skipCanceled || appointment.IsCanceled == false)
                {
                    sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", appointment.AppointmentDate,
                                          appointment.Patient.FullName,
                                          appointment.Patient.Phone,
                                          appointment.Description);
                }
            }

            sb.Append(@"
                                </table>
                            </body>
                        </html>");

            return sb.ToString();
        }

    }
}
