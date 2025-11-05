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
    public class ClienteRepositoryTest
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly SIGEBIContext _context;

        public ClienteRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            var loggerMock = new Mock<ILogger<ClienteRepository>>();
            _context = new SIGEBIContext(options);
            _clienteRepository = new ClienteRepository(_context, loggerMock.Object);
        }

        [Fact]
        public async Task SaveCliente_When_Cliente_IsNull()
        {
            // Arrange
            Cliente cliente = null;

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El objeto Cliente no puede ser nulo.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_Nombre_IsNull()
        {
            // Arrange
            var cliente = new Cliente { Nombre = null };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El nombre es obligatorio.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_Nombre_TooLong()
        {
            // Arrange
            var cliente = new Cliente { Nombre = new string('A', 101) };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El nombre no puede exceder 100 caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_Apellido_TooLong()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Apellido = new string('B', 81)
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El apellido no puede exceder 80 caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_Email_IsNull()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Email = null
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El email es obligatorio.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_Email_InvalidFormat()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Email = "pedrosuarez"
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El email no tiene un formato válido.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_Email_TooLong()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Email = new string('a', 81) + "@gmail.com"
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El email no puede exceder 80 caracteres.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_Nacimiento_IsNull()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Email = "pedro@gmail.com",
                Nacimiento = null
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "La fecha de nacimiento es obligatoria.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_TotalDevoluciones_IsNegative()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Email = "pedro@gmail.com",
                Nacimiento = DateOnly.FromDateTime(DateTime.Now),
                TotalDevoluciones = -1
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "TotalDevoluciones debe ser mayor o igual a 0.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_PrestamosActivos_GreaterThanCapacidad()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Email = "pedro@gmail.com",
                Nacimiento = DateOnly.FromDateTime(DateTime.Now),
                CapacidadPrest = 3,
                PrestamosActivos = 5
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "PrestamosActivos no puede ser mayor que la CapacidadPrest del cliente.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveCliente_When_StatusCliente_IsInvalid()
        {
            // Arrange
            var cliente = new Cliente
            {
                Nombre = "Pedro",
                Email = "pedro@gmail.com",
                Nacimiento = DateOnly.FromDateTime(DateTime.Now),
                StatusCliente = (Status)99
            };

            // Act
            var result = await _clienteRepository.Save(cliente);
            string message = "El StatusCliente no es un valor válido.";

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }
    }
}
