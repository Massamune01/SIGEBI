
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Persistence
{
    public class BibliotecarioRepositoryTest
    {
        private readonly IBibliotecariosRepository _biblioRepository;
        private readonly SIGEBIContext _context;

        public BibliotecarioRepositoryTest()
        {
            // Configurar InMemoryDatabase
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            _context = new SIGEBIContext(options);

            // Mock de ILogger
            var loggerMock = new Mock<ILogger<BibliotecarioRepository>>();

            _biblioRepository = new BibliotecarioRepository(_context, loggerMock.Object);
        }

        [Fact]
        public async Task SaveBiblio_When_Bibliotecario_IsNull()
        {
            // Arrange
            Bibliotecarios biblio = null;

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El objeto Bibliotecarios no puede ser nulo.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveBiblio_When_Nombre_IsNullOrEmpty()
        {
            // Arrange
            var biblio = new Bibliotecarios { Nombre = null };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El nombre es obligatorio.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveBiblio_When_Nombre_TooLong()
        {
            // Arrange
            var biblio = new Bibliotecarios { Nombre = new string('a', 61) };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El nombre no puede exceder 60 caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveBiblio_When_Apellido_TooLong()
        {
            // Arrange
            var biblio = new Bibliotecarios
            {
                Nombre = "Pedro",
                Apellido = new string('a', 81)
            };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El apellido no puede exceder 80 caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveBiblio_When_Email_IsNullOrEmpty()
        {
            // Arrange
            var biblio = new Bibliotecarios
            {
                Nombre = "Pedro",
                Apellido = "Morel",
                Email = null
            };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El correo electrónico es obligatorio.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveBiblio_When_Email_FormatInvalid()
        {
            // Arrange
            var biblio = new Bibliotecarios
            {
                Nombre = "Pedro",
                Apellido = "Morel",
                Email = "pedroexample.com"
            };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El correo electrónico no tiene un formato válido.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveBiblio_When_Email_TooLong()
        {
            // Arrange
            var biblio = new Bibliotecarios
            {
                Nombre = "Pedro",
                Apellido = "Morel",
                Email = new string('a', 81) + "@gmail.com"
            };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El correo electrónico no puede exceder 80 caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveBiblio_When_Totales_Negativos()
        {
            // Arrange
            var biblio = new Bibliotecarios
            {
                Nombre = "Pedro",
                Email = "pedro@gmail.com",
                TotalDevoluciones = -1
            };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "TotalDevoluciones debe ser mayor o igual a 0.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveBiblio_When_Status_Invalid()
        {
            // Arrange
            var biblio = new Bibliotecarios
            {
                Nombre = "Pedro",
                Email = "pedro@gmail.com",
                BiblioEstatus = (Status)99
            };

            // Act
            var result = await _biblioRepository.Save(biblio);
            string message = "El estado del bibliotecario no es válido.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }
    }
}
