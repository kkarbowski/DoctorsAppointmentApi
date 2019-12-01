using AppointmentApi.Business;
using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Tools.Interfaces;
using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using Moq;
using System;
using Xunit;

namespace AppointmentApiUT
{
    public class UserBusinessUT
    {
        [Fact]
        public void AlwaysPass() { }

        [Theory]
        [InlineData("Login", "Password")]
        public void RegisterUser(string login, string password)
        {
            // Arrange
            var user = new User() { Login = login, Password = password };

            var userDataAccessMock = new Mock<IUserDataAccess>();
            userDataAccessMock.Setup(x => x.AddUser(user)).Returns(new User { Login = login, Password = "hashed" });

            var hashGeneratorMock = new Mock<IHashGenerator>();
            hashGeneratorMock.Setup(x => x.GenerateHash(user.Password)).Returns("hashed");

            var tokenGeneratorMock = new Mock<ITokenGenerator>();

            var userBusiness = new UserBusiness(hashGeneratorMock.Object, userDataAccessMock.Object, tokenGeneratorMock.Object);

            // Act
            var registeredUser = userBusiness.Register(user);

            // Assert
            Assert.NotNull(registeredUser);
        }

        [Theory]
        [InlineData("Login", "Password")]
        public void LoginUser(string login, string password)
        {
            // Arrange
            var user = new User() { Login = login, Password = password };

            var userDataAccessMock = new Mock<IUserDataAccess>();
            userDataAccessMock.Setup(x => x.GetUser(user.Login)).Returns(new User { Login = login, Password = "hashed" });

            var hashGeneratorMock = new Mock<IHashGenerator>();
            hashGeneratorMock.Setup(x => x.GenerateHash(user.Password)).Returns("hashed");

            var tokenGeneratorMock = new Mock<ITokenGenerator>();
            var tokenExpiration = DateTime.Now.AddDays(1);
            var generatedToken = new Token { Expiration = tokenExpiration, TokenString = "tokenStringValue" };
            tokenGeneratorMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns(new Token { Expiration = generatedToken.Expiration, TokenString = generatedToken.TokenString });

            var userBusiness = new UserBusiness(hashGeneratorMock.Object, userDataAccessMock.Object, tokenGeneratorMock.Object);

            // Act
            var returnedToken = userBusiness.Login(user);

            // Assert
            Assert.Equal(generatedToken.Expiration, returnedToken.Expiration);
            Assert.Equal(generatedToken.TokenString, returnedToken.TokenString);
        }
    }
}
