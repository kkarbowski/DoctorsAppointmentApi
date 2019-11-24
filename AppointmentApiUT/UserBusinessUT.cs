using AppointmentApi.Business;
using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Tools.Interfaces;
using AppointmentModel.Model;
using Moq;
using System;
using Xunit;

namespace AppointmentApiUT
{
    public class UserBusinessUT
    {
        [Fact]
        public void AlwaysPass()
        {

        }

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

        //[Theory]
        //[InlineData("Login", "Password")]
        //public void LoginUser(string login, string password)
        //{
        //    // Arrange
        //    var user = new User() { Login = login, Password = password }
        //    var userDataAccessMock = new Mock<IUserDataAccess>();
        //    userDataAccessMock.Setup(x => x.GetUser(user.Login))
        //    var userBusiness = new UserBusiness();

        //    // Act

        //    // Assert
        //    Exception ex = Assert
        //     .Throws<DivideByZeroException>(() => cs.UnsafeDivide(x, y));

        //}
    }
}
