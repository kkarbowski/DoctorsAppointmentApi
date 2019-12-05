using AppointmentApi.Authorization;
using AppointmentApi.Business;
using AppointmentApi.Controllers;
using AppointmentApi.DataAccess;
using AppointmentApi.DataAccess.Interfaces;
using AppointmentApi.Exceptions;
using AppointmentApi.Tools.Interfaces;
using AppointmentModel.Model;
using AppointmentModel.ReturnModel;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace AppointmentApiUT
{
    public class AppointmentControllerUT
    {
        [Fact]
        public void AlwaysPass() { }

        [Theory]
        [InlineData(12)]
        [InlineData(6132)]
        public void GetAppointmentForExistingAppointmentId(int searchedAppointmentId)
        {
            // Arrange
            var patientAuthorizationMock = new Mock<IPatientAuthorization>();
            var pdfGeneratorMock = new Mock<IPdfGenerator>();
            var converterMock = new Mock<IConverter>();

            var appointmentBusinessMock = new Mock<IAppointmentBusiness>();
            appointmentBusinessMock.Setup(x => x.GetAppointment(searchedAppointmentId)).Returns(new Appointment { AppointmentId = searchedAppointmentId });

            var appointmentController = new AppointmentController(appointmentBusinessMock.Object, patientAuthorizationMock.Object, pdfGeneratorMock.Object, converterMock.Object);

            // Act
            var appointmentResponse = appointmentController.GetAppointment(searchedAppointmentId);

            // Assert
            Assert.IsType<OkObjectResult>(appointmentResponse);
            Assert.IsType<Appointment>(((OkObjectResult)appointmentResponse).Value);
            Assert.Equal(((Appointment)((OkObjectResult)appointmentResponse).Value).AppointmentId, searchedAppointmentId);
        }

        [Fact]
        public void GetAppointmentForNotExistingAppointmentId()
        {
            // Arrange
            var patientAuthorizationMock = new Mock<IPatientAuthorization>();
            var pdfGeneratorMock = new Mock<IPdfGenerator>();
            var converterMock = new Mock<IConverter>();

            var appointmentBusinessMock = new Mock<IAppointmentBusiness>();
            appointmentBusinessMock.Setup(x => x.GetAppointment(default)).Throws(new EntityNotFoundException("Message", new object()));

            var appointmentController = new AppointmentController(appointmentBusinessMock.Object, patientAuthorizationMock.Object, pdfGeneratorMock.Object, converterMock.Object);

            // Act
            Action getAppointmentForNotExistingAppointmentIdAction = () => appointmentController.GetAppointment(default);

            // Assert
            var exception = Assert.Throws<EntityNotFoundException>(getAppointmentForNotExistingAppointmentIdAction);
            Assert.NotEmpty(exception.Message);
        }
    }
}
