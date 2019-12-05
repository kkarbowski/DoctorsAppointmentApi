using AppointmentApi.Business;
using AppointmentApi.DataAccess;
using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Exceptions;
using AppointmentApi.Tools.Interfaces;
using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using Moq;
using System;
using Xunit;

namespace AppointmentApiUT
{
    public class AppointmentBusinessUT
    {
        [Fact]
        public void AlwaysPass() { }

        [Theory]
        [InlineData(1)]
        [InlineData(132)]
        public void GetAppointmentForExistingAppointmentId(int searchedAppointmentId)
        {
            // Arrange
            var appointmentDataAccessMock = new Mock<IAppointmentDataAccess>();
            appointmentDataAccessMock.Setup(x => x.GetAppointment(searchedAppointmentId)).Returns(new Appointment { AppointmentId = searchedAppointmentId });

            var appointmentBusiness = new AppointmentBusiness(appointmentDataAccessMock.Object);

            // Act
            var databaseAppointment = appointmentBusiness.GetAppointment(searchedAppointmentId);

            // Assert
            Assert.NotNull(databaseAppointment);
        }

        [Fact]
        public void GetAppointmentForNotExistingAppointmentId()
        {
            // Arrange
            var appointmentDataAccessMock = new Mock<IAppointmentDataAccess>();
            appointmentDataAccessMock.Setup(x => x.GetAppointment(default)).Returns((Appointment)null);

            var appointmentBusiness = new AppointmentBusiness(appointmentDataAccessMock.Object);

            // Act
            Func<Appointment> getAppointmentForNotExistingAppointmentIdAction = () => appointmentBusiness.GetAppointment(default);

            // Assert
            var exception = Assert.Throws<EntityNotFoundException>(getAppointmentForNotExistingAppointmentIdAction);
            Assert.NotEmpty(exception.Message);
        }
    }
}
