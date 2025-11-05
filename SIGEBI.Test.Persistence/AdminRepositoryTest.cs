using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;
using SIGEBI.Persistence.Security.Configuration.ValidarAdmin;

namespace SIGEBI.Test.Persistence
{
    public class AdminRepositoryTest
    {
        private readonly IAdminRepository _adminRepository;
        private readonly SIGEBIContext _context;

        public AdminRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            var  loggerMock= new Mock<ILogger<AdminRepository>>();
            _context = new SIGEBIContext(options);
            _adminRepository = new AdminRepository(_context, loggerMock.Object);
        }


        [Fact]
        public async Task SaveAdmin_When_AdminEntity_IsNull()
        {
            // Arrange
             Admin admin = null;
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El objeto Admin no puede ser nulo.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_When_Nombre_IsNullorEmpty()
        {
            // Arrange
            Admin admin = new Admin() { Nombre = null};
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El nombre es obligatorio.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_When_Nombre_MoreWords()
        {
            // Arrange
            Admin admin = new Admin() { Nombre = "ddddddddddddddddddddddddddddddddddddddddasdasdasdsadadddddddddddddddddddddddddddddddddddddddddddd" };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El nombre no puede exceder el limite de caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_When_Apellido_MoreWords()
        {
            // Arrange
            Admin admin = new Admin() { Nombre = "pedro",Apellido = "ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddasdasdsdsadadadasdasdsadasdadasdasdsadads" };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El apellido no puede exceder el limite de caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_When_Email_IsNullorEmpty()
        {
            // Arrange
            Admin admin = new Admin() { Nombre = "pedro", 
                Apellido="hola@gmail.com",Email = null };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El email es obligatorio.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }
        [Fact]
        public async Task SaveAdmin_When_Email_Formato_isValid()
        {
            // Arrange
            Admin admin = new Admin() {
                Nombre = "pedro",
                Apellido = "morel",
                Email = "pedrosuarez" };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El email no tiene un formato válido.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }
        [Fact]
        public async Task SaveAdmin_When_Email_length_isToLong()
        {
            // Arrange
            Admin admin = new Admin() { Nombre = "pedro",
                Apellido = "morel",
                Email = "dddddddddddddddddddddddddddddddddddddddddddddddddddddddasasssssssssssssssssssssssssssssssssssssssssssssssssssssss@gmail.com" };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El email no puede exceder el limite de caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_When_Nacimiento_IsNullorEmpty()
        {
            // Arrange
            DateOnly? dateOnly = null;
            Admin admin = new Admin() {
                Nombre = "pedro",
                Apellido = "morel",
                Email = "pedrosuarez@gmail.com",Nacimiento = dateOnly };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "La fecha de nacimiento es obligatoria.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_When_Nacimiento_is_OnTheFuture()
        {
            // Arrange
            Admin admin = new Admin() {
                Nombre = "pedro",
                Apellido = "morel",
                Email = "pedrosuarez@gmail.com",
                Nacimiento = DateOnly.FromDateTime(DateTime.Now.AddDays(1))};
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "La fecha de nacimiento no puede ser futura.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveAdmin_When_Edad_isNegative()
        {
            // Arrange
            Admin admin = new Admin() { Nombre = "Loca",
                Email = "pedrosuarez@gmail.com",Nacimiento=DateOnly.FromDateTime(DateTime.Now),
                Edad = -2 };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "La edad no puede ser negativa.";
            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }
        [Fact]
        public async Task SaveAdmin_When_Edad_isTooOld()
        {
            // Arrange
            Admin admin = new Admin() {
                Nombre = "pedro",
                Apellido = "morel",
                Email = "pedrosuarez@gmail.com", 
                Nacimiento = DateOnly.FromDateTime(DateTime.Now),
                Edad = 150 };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "La edad no puede ser mayor de 150 años.";
            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }
        [Fact]
        public async Task SaveAdmin_When_Status_IsNullorEmpty()
        {
            // Arrange
            Admin admin = new Admin() {
                Nombre = "pedro",
                Apellido = "morel",
                Email = "pedrosuarez@gmail.com",
                Nacimiento = DateOnly.FromDateTime(DateTime.Now),
                AdminEstatus = (Status)3 };
            // Act
            var result = await _adminRepository.Save(admin);
            string message = "El estado del admin no es válido.";
            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }
    }
}