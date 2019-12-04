using AppointmentModel.Model;
using AppointmentRazor.Areas.Patients.Pages;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Authentication;
using AppointmentRazor.Utilities.Localization.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace FrontTests
{
    public class MakeAppointmentFormTest
    {
        [Fact]
        public async Task OnPostAsync_Incorrect_DateFromPast()
        {
            //// Arrange
            var authenticationServiceMock = new Mock<IAppointmentsService>();

            var availableDoctors = new List<Doctor>();
            authenticationServiceMock.Setup(x => x.GetAllAvailableDoctors()).ReturnsAsync(availableDoctors);

            var allReasons = new List<Reason>();
            authenticationServiceMock.Setup(x => x.GetAllAppointmentReasons()).ReturnsAsync(allReasons);

            var localizerMock = new Mock<ICultureLocalizer>();
            localizerMock.Setup(x => x.Text(It.IsAny<string>())).Returns(new LocalizedString("name", "test"));

            var appointmentModel = new MakeAnAppointmentModel(localizerMock.Object, authenticationServiceMock.Object)
            {
                Appointment = new Appointment()
            };

            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext);

            appointmentModel.PageContext = pageContext;

            var pickedDate = DateTime.MinValue;

            //// Act
            var response = await appointmentModel.OnPostAsync(0, new int[2] { 1, 2}, pickedDate);

            //// Assert
            Assert.False(appointmentModel.ModelState.IsValid);
        }

        [Fact]
        public async Task OnPostAsync_Incorrect_TerminAlreadyUsed()
        {
            //// Arrange
            var authenticationServiceMock = new Mock<IAppointmentsService>();

            var appointmentResponse = AppointmentSetResponse.DATE_NOT_AVAILABLE;

            authenticationServiceMock.Setup(x => x.SetAppointment(It.IsAny<Appointment>())).ReturnsAsync(appointmentResponse);
            var availableDoctors = new List<Doctor>();
            authenticationServiceMock.Setup(x => x.GetAllAvailableDoctors()).ReturnsAsync(availableDoctors);

            var allReasons = new List<Reason>();
            authenticationServiceMock.Setup(x => x.GetAllAppointmentReasons()).ReturnsAsync(allReasons);

            var localizerMock = new Mock<ICultureLocalizer>();
            localizerMock.Setup(x => x.Text(It.IsAny<string>())).Returns(new LocalizedString("name", "test"));

            var appointmentModel = new MakeAnAppointmentModel(localizerMock.Object, authenticationServiceMock.Object)
            {
                Appointment = new Appointment()
            };
            var features = new FeatureCollection();
            var session = new DefaultSessionFeature();
            features.Set<ISessionFeature>(session);
            var httpContext = new DefaultHttpContext(features);
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext);

            appointmentModel.PageContext = pageContext;

            var pickedDate = DateTime.UtcNow.AddDays(2);

            //// Act
            var response = await appointmentModel.OnPostAsync(0, new int[2] { 1, 2 }, pickedDate);

            //// Assert
            Assert.True(appointmentModel.ModelState.IsValid);
            Assert.IsType<PageResult>(response);
        }
    }
}
