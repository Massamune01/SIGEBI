using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Validators.Configuration.AdminValidators;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Application
{
    public class AdminServiceTest
    {
        private readonly IAdminFacade _adminFacade;
        private readonly SIGEBIContext _context;

        public AdminServiceTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            _context = new SIGEBIContext(options);
            var loggerMock = new Mock<ILogger<AdminFacade>>();
            var loggerMock1 = new Mock<ILogger<AdminValidator>>();
            var loggerMock2 = new Mock<ILogger<AdminRepository>>();
            var repository = new AdminRepository(_context, loggerMock2.Object);
            var validator = new AdminValidator(repository, loggerMock1.Object);
            var mapper = new Mock<IMapper>();
            _adminFacade = new AdminFacade(repository, validator, loggerMock.Object);
        }

        [Fact]
        public async Task SaveAdmin_Check_If_Nombre_Is_Required()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20))
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Nombre is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Apellido is required
        public async Task SaveAdmin_Check_If_Apellido_Is_Required()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20))
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Apellido is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_Check_If_Edad_Is_At_Least_17()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 16,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-16))
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Edad must be at least 17.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_Check_If_Nacimiento_Is_Not_In_The_Future()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(1))
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Nacimiento cannot be in the future.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        // Check that Nacimiento is not the same as today
        public async Task SaveAdmin_Check_If_Nacimiento_Is_Not_Today()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now)
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Nacimiento cannot be today's date.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check if Cedula is not null or empty
        public async Task SaveAdmin_Check_If_Cedula_Is_Required()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20))
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Cedula is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_Check_If_Cedula_Is_11_Characters_Long()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                Cedula = "123456789" // 9 characters
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Cedula needs to be 11 digits";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_Check_If_Cedula_Is_In_Use()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                Cedula = "40294598714",
                Email = "pedroPuto@gmail.com"
            };
            var dtoAdmin1 = new AdminCreateDto
            {
                Nombre = "Camello",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                Cedula = "40294598714",
                Email = "pedroPuo@gmail.com"
            };

            // Act
            await _adminFacade.CreateAdminAsync(dtoAdmin);
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin1);
            string message = "Cedula is already in use.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_Check_If_Email_Is_Not_NullorEmpty()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                Cedula = "40214024605",
                Email = ""
            };
            // Act
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin);
            string message = "Email is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_Check_If_Email_Is_In_Use()
        {
            // Arrange
            var dtoAdmin = new AdminCreateDto
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                Cedula = "40214024605",
                Email = "pedro.suarez@gmail.com"
            };
            var dtoAdmin1 = new AdminCreateDto
            {
                Nombre = "Camello",
                Apellido = "Cordero",
                Edad = 20,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-20)),
                Cedula = "40214024606",
                Email = "pedro.suarez@gmail.com"
            };

            // Act
            await _adminFacade.CreateAdminAsync(dtoAdmin);
            var result = await _adminFacade.CreateAdminAsync(dtoAdmin1);
            string message = "Email is already in use.";

            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

    }
}
