using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Domain.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Persistence
{
    public class LogOperationRepositoryTest
    {
        private readonly LogOperationsRepository _logRepository;
        private readonly SIGEBIContext _context;

        public LogOperationRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase(databaseName: "SIGEBI_LogTest")
                .Options;

            var loggerMock = new Mock<ILogger<LogOperationsRepository>>();
            _context = new SIGEBIContext(options);
            _logRepository = new LogOperationsRepository(_context, loggerMock.Object);
        }

        [Fact]
        public async Task SaveLogOperation_When_Object_IsNull()
        {
            // Arrange
            LogOperations log = null;

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("El objeto LogOperation no puede ser nulo.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_Entity_IsEmpty()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = "",
                Fecha = DateTime.Now,
                TypeOperation = "Insert"
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("El campo Entity no puede estar vacío.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_Entity_IsTooLong()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = new string('A', 91),
                Fecha = DateTime.Now,
                TypeOperation = "Insert"
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("El campo Entity no puede exceder los 90 caracteres.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_Fecha_IsFuture()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = "Admin",
                Fecha = DateTime.Now.AddDays(1),
                TypeOperation = "Update"
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("La fecha no puede estar en el futuro.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_TypeOperation_IsTooLong()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = "Admin",
                Fecha = DateTime.Now,
                TypeOperation = new string('B', 101)
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("El campo TypeOperation no puede exceder los 100 caracteres.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_Descripcion_IsTooLong()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = "Admin",
                Fecha = DateTime.Now,
                TypeOperation = "Insert",
                Descripcion = new string('C', 101)
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("La descripción no puede exceder los 100 caracteres.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_LastUpdateBy_IsFuture()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = "Admin",
                Fecha = DateTime.Now,
                TypeOperation = "Insert",
                LastUpdateBy = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("LastUpdateBy no puede estar en el futuro.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_UpdateBy_IsFuture()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = "Admin",
                Fecha = DateTime.Now,
                TypeOperation = "Insert",
                UpdateBy = DateTime.Now.AddDays(1)
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("UpdateBy no puede estar en el futuro.", result.Message);
        }

        [Fact]
        public async Task SaveLogOperation_When_AllFieldsAreValid()
        {
            // Arrange
            var log = new LogOperations
            {
                Entity = "Admin",
                Fecha = DateTime.Now,
                TypeOperation = "Insert",
                Descripcion = "Registro exitoso",
                LastUpdateBy = DateTime.Now.AddDays(-1),
                UpdateBy = DateTime.Now.AddDays(-2)
            };

            // Act
            var result = await _logRepository.Save(log);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Data);
        }
    }
}
