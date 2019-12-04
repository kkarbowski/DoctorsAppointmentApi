using AppointmentModel.Model;
using AppointmentRazor.Pages;
using AppointmentRazor.Services.Interfaces;
using AppointmentRazor.Utilities.Localization;
using AppointmentRazor.Utilities.Localization.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static AppointmentRazor.Pages.LoginModel;

namespace FrontTests
{
    public class LoginFormTest
    {
        [Fact]
        public async Task OnPostAsync_Correct_Login()
        {
            //// Arrange
            var authenticationResponse = new AuthenticationReponse() 
            { 
                WasAuthenticationCorrect = true 
            };

            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(x => x.Login(It.IsAny<User>())).ReturnsAsync(authenticationResponse);

            var localizerMock = new Mock<ICultureLocalizer>();
            localizerMock.Setup(x => x.Text(It.IsAny<string>())).Returns(new LocalizedString("name", "test"));

            var loginModel = new LoginModel(localizerMock.Object, authenticationServiceMock.Object);

            loginModel.LoginForm = new _LoginForm() { Username = "user", Password = "password" };
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext);

            loginModel.PageContext = pageContext;

            //// Act
            await loginModel.OnPostAsync();

            //// Assert
            Assert.Null(loginModel.Msg);
        }

        [Fact]
        public async Task OnPostAsync_Incorrect_Login()
        {
            //// Arrange
            var authenticationResponse = new AuthenticationReponse()
            {
                WasAuthenticationCorrect = false
            };

            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(x => x.Login(It.IsAny<User>())).ReturnsAsync(authenticationResponse);

            var localizerMock = new Mock<ICultureLocalizer>();
            localizerMock.Setup(x => x.Text(It.IsAny<string>())).Returns(new LocalizedString("name", "localizedMessage"));

            var loginModel = new LoginModel(localizerMock.Object, authenticationServiceMock.Object);

            loginModel.LoginForm = new _LoginForm() { Username = "user", Password = "password" };
            var httpContext = new DefaultHttpContext();
            var modelState = new ModelStateDictionary();
            var actionContext = new ActionContext(httpContext, new RouteData(), new PageActionDescriptor(), modelState);
            var pageContext = new PageContext(actionContext);

            loginModel.PageContext = pageContext;

            //// Act
            await loginModel.OnPostAsync();

            //// Assert
            Assert.Equal("localizedMessage", loginModel.Msg);
        }
    }
}
