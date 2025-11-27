using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Validators.Configuration.AdminValidators;
using SIGEBI.Application.Validators.Configuration.ClienteValidators;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Application
{
    public class ClienteServiceTest
    {
        private readonly IClienteFacade _clienteFacade;
        private readonly SIGEBIContext _context;

        public ClienteServiceTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            _context = new SIGEBIContext(options);
            var loggerMock = new Mock<ILogger<ClienteFacade>>();
            var loggerMock1 = new Mock<ILogger<ClienteValidator>>();
            var loggerMock2 = new Mock<ILogger<ClienteRepository>>();
            var repository = new ClienteRepository(_context, loggerMock2.Object);
            var validator = new ClienteValidator(repository, loggerMock1.Object);
            var mapper = new Mock<IMapper>();
            _clienteFacade = new ClienteFacade(repository, loggerMock.Object, validator);
        }

        [Fact]
        public async Task SaveCliente_Check_If_Nombre_Is_Required()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-25))
            };
            // Act
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente);
            string message = "Nombre is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Apellido is required
        public async Task SaveCliente_Check_If_Apellido_Is_Required()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-25))
            };
            // Act
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente);
            string message = "Apellido is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Edad must be at least 17
        public async Task SaveCliente_Check_If_Edad_Is_At_Least_17()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 16,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-16))
            };
            // Act
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente);
            string message = "Edad must be at least 17.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Nacimiento cannot be in the future
        public async Task SaveCliente_Check_If_Nacimiento_Is_Not_In_The_Future()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(1))
            };
            // Act
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente);
            string message = "Nacimiento cannot be in the future.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Nacimiento cannot be today's date
        public async Task SaveCliente_Check_If_Nacimiento_Is_Not_Todays_Date()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now)
            };
            // Act
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente);
            string message = "Nacimiento cannot be today's date.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        // Check if Cedula is not null or empty
        public async Task SaveCliente_Check_If_Cedula_Is_Required()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
                Cedula = ""
            };
            // Act
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente);
            string message = "Cedula is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        // Check if Cedula is unique
        public async Task SaveCliente_Check_If_Cedula_Is_Unique()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
                Cedula = "23456789101",
                Email = "carlosPuto@gmail.com"
            };
            var dtoCliente1 = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
                Cedula = "23456789101",
                Email = "carlosPuto@gmail.com"
            };
            // Act
            await _clienteFacade.CreateClienteAsync(dtoCliente);
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente1);
            string message = "Cedula is already in use.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        // Check if Email is not null or empty
        public async Task SaveCliente_Check_If_Email_Is_Required()
        {
            // Arrange
            var dtoCliente = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
                Cedula = "12345678911",
                Email = ""
            };
            // Act
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente);
            string message = "Email is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        // Check if Email is unique
        public async Task SaveCliente_Check_If_Email_Is_Unique()
        {
            // Arrange
            var dtoCliente1 = new ClienteCreateDto
            {
                Nombre = "Carlos",
                Apellido = "Gomez",
                Edad = 25,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-25)),
                Cedula = "12345678912",
                Email = "pedroJuan@gmail.com"
            };
            var dtoCliente2 = new ClienteCreateDto
            {
                Nombre = "Ana",
                Apellido = "Lopez",
                Edad = 30,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Cedula = "12345678913",
                Email = "pedroJuan@gmail.com"
            };

            // Act
            await _clienteFacade.CreateClienteAsync(dtoCliente1);
            var result = await _clienteFacade.CreateClienteAsync(dtoCliente2);
            string message = "Email is already in use.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);

        }
    }
}
