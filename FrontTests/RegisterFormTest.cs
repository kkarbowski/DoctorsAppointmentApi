using AppointmentModel;
using AppointmentRazor.Pages;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization.Interfaces;
using AppointmentRazor.ViewModels;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FrontTests
{
    public class RegisterFormTest
    {
        [Fact]
        public async Task OnPostAsync_Correct_Registration()
        {
            //// Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(x => x.Register(It.IsAny<Patient>())).ReturnsAsync(true);

            var localizerMock = new Mock<ICultureLocalizer>();
            localizerMock.Setup(x => x.Text(It.IsAny<string>())).Returns(new LocalizedString("name", "test"));

            var registerModel = new RegisterModel(localizerMock.Object, authenticationServiceMock.Object)
            {
                RegistrationForm = new RegistrationForm() { Birthday = DateTime.MinValue}
            };
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext);

            registerModel.PageContext = pageContext;

            //// Act
            await registerModel.OnPostAsync();

            //// Assert
            Assert.Null(registerModel.Msg);
        }

        [Fact]
        public async Task OnPostAsync_Incorrect_Registration()
        {
            //// Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(x => x.Register(It.IsAny<Patient>())).ReturnsAsync(false);

            var localizerMock = new Mock<ICultureLocalizer>();
            localizerMock.Setup(x => x.Text(It.IsAny<string>())).Returns(new LocalizedString("name", "test"));

            var registerModel = new RegisterModel(localizerMock.Object, authenticationServiceMock.Object)
            {
                RegistrationForm = new RegistrationForm() { Birthday = DateTime.MinValue }
            };

            //// Act
            await registerModel.OnPostAsync();

            //// Assert
            Assert.NotNull(registerModel.Msg);
        }

        [Fact]
        public async Task OnPostAsync_Incorrect_Registration_InvalidDate()
        {
            //// Arrange
            var authenticationServiceMock = new Mock<IAuthenticationService>();

            var localizerMock = new Mock<ICultureLocalizer>();
            localizerMock.Setup(x => x.Text(It.IsAny<string>())).Returns(new LocalizedString("name", "test"));

            var registerModel = new RegisterModel(localizerMock.Object, authenticationServiceMock.Object)
            {
                RegistrationForm = new RegistrationForm() { Birthday = DateTime.UtcNow.AddDays(2) }
            };

            //// Act
            await registerModel.OnPostAsync();

            //// Assert
            Assert.False(registerModel.ModelState.IsValid);
        }
    }
}
