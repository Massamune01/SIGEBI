using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Validators.Configuration.BibliotecarioValidators;
using SIGEBI.Application.Validators.Configuration.LogOpValidators;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Application
{
    public class BibliotecarioServicesTest
    {
        private readonly IBibliotecarioFacade _biblioFacade;
        private readonly SIGEBIContext _context;

        public BibliotecarioServicesTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            _context = new SIGEBIContext(options);
            var loggerMock = new Mock<ILogger<BibliotecarioFacade>>();
            var loggerMock1 = new Mock<ILogger<BibliotecarioValidator>>();
            var loggerMock2 = new Mock<ILogger<BibliotecarioRepository>>();
            var repository = new BibliotecarioRepository(_context, loggerMock2.Object);
            var validator = new BibliotecarioValidator(repository, loggerMock1.Object);
            var mapper = new Mock<IMapper>();
            _biblioFacade = new BibliotecarioFacade(repository, validator, loggerMock.Object);
        }

        [Fact]
        //Check if Cedula is already in use
        public async Task SaveBibliotecario_Check_If_Cedula_Is_Already_In_Use()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioCreateDto
            {
                Nombre = "Caraballo",
                Apellido = "Lopez",
                Cedula = "12345678910",
                Edad = 30,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Email = "Caraballo@gmail.com"
            };
            var dtoBibliotecario1 = new BibliotecarioCreateDto
            {
                Nombre = "Caraballo",
                Apellido = "Lopez",
                Cedula = "12345678910",
                Edad = 30,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-30)),
                Email = "Caraballo@gmail.com"
            };

            // Act
            await _biblioFacade.CreateBibliotecarioAsync(dtoBibliotecario);
            var result = await _biblioFacade.CreateBibliotecarioAsync(dtoBibliotecario1);
            string message = "Cedula is already in use.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Email is required
        public async Task SaveBibliotecario_Check_If_Email_Is_Required()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioCreateDto
            {
                Nombre = "Maria",
                Apellido = "Gonzalez",
                Cedula = "10987654321",
                Edad = 28,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-28)),
                Email = ""
            };
            // Act
            var result = await _biblioFacade.CreateBibliotecarioAsync(dtoBibliotecario);
            string message = "Email is required.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check if email is already in use
        public async Task SaveBibliotecario_Check_If_Email_Is_Already_In_Use()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioCreateDto
            {
                Nombre = "Luis",
                Apellido = "Martinez",
                Cedula = "11223344556",
                Edad = 35,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-35)),
                Email = "LuisRodriguez@gmail.com"
            };

            var dtoBibliotecario1 = new BibliotecarioCreateDto
            {
                Nombre = "Ana",
                Apellido = "Rodriguez",
                Cedula = "66554433221",
                Edad = 32,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-32)),
                Email = "LuisRodriguez@gmail.com"
            };

            // Act
            await _biblioFacade.CreateBibliotecarioAsync(dtoBibliotecario);
            var result = await _biblioFacade.CreateBibliotecarioAsync(dtoBibliotecario1);
            string message = "Email is already in use.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }


        [Fact]
        //Check that Email is required in Update
        public async Task UpdateBibliotecario_Check_If_Email_Is_Required()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioUpdateDto
            {
                Nombre = "Carlos",
                Apellido = "Sanchez",
                Cedula = "99887766554",
                Edad = 29,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-29)),
                Email = ""
            };
            // Act
            var result = await _biblioFacade.UpdateBibliotecarioAsync(dtoBibliotecario);
            string message = "Email is required.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check if TotalDevoluciones cannot be negative on Update
        public async Task UpdateBibliotecario_Check_If_TotalDevoluciones_Cannot_Be_Negative()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioUpdateDto
            {
                Nombre = "Sofia",
                Apellido = "Fernandez",
                Cedula = "22334455667",
                Edad = 27,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-27)),
                Email = "SofiaF@gmail.com",
                TotalDevoluciones = -5
            };
            // Act
            var result = await _biblioFacade.UpdateBibliotecarioAsync(dtoBibliotecario);
            string message = "TotalDevoluciones cannot be negative.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check if TotalPrestamos cannot be negative on Update

        public async Task UpdateBibliotecario_Check_If_TotalPrestamos_Cannot_Be_Negative()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioUpdateDto
            {
                Nombre = "Miguel",
                Apellido = "Torres",
                Cedula = "33445566778",
                Edad = 31,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-31)),
                Email = "SofiaF@gmail.com",
                TotalDevoluciones = 5,
                TotalPrestamos = -10
            };
            // Act
            var result = await _biblioFacade.UpdateBibliotecarioAsync(dtoBibliotecario);
            string message = "TotalPrestamos cannot be negative.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check if TotalDevoluciones cannot be greater than TotalPrestamos on Update
        public async Task UpdateBiblio_Check_If_TotalPrestamos_Is_Less_Than_TotalDevoluciones()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioUpdateDto
            {
                Nombre = "Miguel",
                Apellido = "Torres",
                Cedula = "33445566778",
                Edad = 31,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-31)),
                Email = "SofiaF@gmail.com",
                TotalDevoluciones = 10,
                TotalPrestamos = 5,
            };
            // Act
            var result = await _biblioFacade.UpdateBibliotecarioAsync(dtoBibliotecario);
            string message = "TotalDevoluciones cannot be greater than TotalPrestamos.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //TotalClientesAtendidos cannot be negative.
        public async Task UpdateBibliotecario_Check_If_TotalClientesAtendidos_Cannot_Be_Negative()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioUpdateDto
            {
                Nombre = "Miguel",
                Apellido = "Torres",
                Cedula = "33445566778",
                Edad = 31,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-31)),
                Email = "SofiaF@gmail.com",
                TotalDevoluciones = 10,
                TotalPrestamos = 10,
                TotalClientesAtendidos = -3
            };
            // Act
            var result = await _biblioFacade.UpdateBibliotecarioAsync(dtoBibliotecario);
            string message = "TotalClientesAtendidos cannot be negative.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //TotalHorasTrabajadas cannot be negative.
        public async Task UpdateBibliotecario_Check_If_TotalHorasTrabajadas_Cannot_Be_Negative()
        {
            // Arrange
            var dtoBibliotecario = new BibliotecarioUpdateDto
            {
                Nombre = "Miguel",
                Apellido = "Torres",
                Cedula = "33445566778",
                Edad = 31,
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-31)),
                Email = "SofiaF@gmail.com",
                TotalDevoluciones = 10,
                TotalPrestamos = 10,
                TotalClientesAtendidos = -3
            };
            // Act
            var result = await _biblioFacade.UpdateBibliotecarioAsync(dtoBibliotecario);
            string message = "TotalClientesAtendidos cannot be negative.";
            // Assert
            Assert.IsType<SIGEBI.Application.Base.ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);

        }
    }
}

