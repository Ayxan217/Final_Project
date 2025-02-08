using FinalProject.Application.DTOs.Appointment;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinalProject.Tests
{
    public class AppointmentsControllerTests
    {
        private readonly Mock<IAppointmentService> _mockAppointmentService;
        private readonly AppointmentsController _controller;

        public AppointmentsControllerTests()
        {
            _mockAppointmentService = new Mock<IAppointmentService>();
            _controller = new AppointmentsController(_mockAppointmentService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfAppointments()
        {
            // Arrange
            var appointments = new List<AppointmentDto>
        {
            new AppointmentDto
            {
                Id = 1,
                DoctorId = 1,
                DoctorName = "Dr. Smith",
                PatientId = 1,
                PatientName = "John Doe",
                AppointmentDate = DateTime.Now.AddDays(1),
                Description = "Regular checkup"
            }
        };

            _mockAppointmentService.Setup(service =>
                service.GetAllAppointmentsWithDetailsAsync())
                    .ReturnsAsync(appointments);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAppointments = Assert.IsAssignableFrom<IEnumerable<AppointmentDto>>(okResult.Value);
            Assert.Single(returnedAppointments);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenAppointmentExists()
        {
            // Arrange
            var appointmentDto = new AppointmentDto
            {
                Id = 1,
                DoctorId = 1,
                DoctorName = "Dr. Smith",
                PatientId = 1,
                PatientName = "John Doe",
                AppointmentDate = DateTime.Now.AddDays(1)
            };

            _mockAppointmentService.Setup(service =>
                service.GetAppointmentByIdWithDetailsAsync(1))
                    .ReturnsAsync(appointmentDto);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedAppointment = Assert.IsType<AppointmentDto>(okResult.Value);
            Assert.Equal(1, returnedAppointment.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            _mockAppointmentService.Setup(service =>
                service.GetAppointmentByIdWithDetailsAsync(1))
                    .ReturnsAsync((AppointmentDto)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenModelStateIsValid()
        {
            // Arrange
            var createDto = new CreateAppointmentDto
            {
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                Description = "Checkup"
            };

            _mockAppointmentService.Setup(service =>
                service.CreateAppointmentAsync(createDto))
                    .ReturnsAsync(1);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
            Assert.Equal(1, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updateDto = new UpdateAppointmentDto
            {
                Id = 1,
                DoctorId = 1,
                PatientId = 1,
                AppointmentDate = DateTime.Now.AddDays(1),
                Description = "Updated checkup"
            };

            _mockAppointmentService.Setup(service =>
                service.UpdateAppointmentAsync(updateDto))
                    .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(1, updateDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var updateDto = new UpdateAppointmentDto { Id = 2 };

            // Act
            var result = await _controller.Update(1, updateDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            _mockAppointmentService.Setup(service =>
                service.DeleteAppointmentAsync(1))
                    .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            // Arrange
            _mockAppointmentService.Setup(service =>
                service.DeleteAppointmentAsync(1))
                    .ThrowsAsync(new NotFoundException("Appointment not found"));

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
