using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Configuration.PrestamosValidators;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Interfaces.Cache;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Application
{
    public class PrestamoServicesTest
    {
        private readonly IPrestamosService _prestamosServices;
        private readonly SIGEBIContext _context;

        public PrestamoServicesTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            var loggerMock = new Mock<ILogger<PrestamosServices>>();
            var IprestamosRepository = new Mock<IPrestamosRepository>();
            var createvalidator = new PrestamoCreateValidator(IprestamosRepository.Object, new Mock<ILogger<PrestamoCreateValidator>>().Object);
            var updatevalidator = new PrestamoUpdateValidator(IprestamosRepository.Object, new Mock<ILogger<PrestamoUpdateValidator>>().Object);
            var cacheservice = new Mock<ICacheService>();
            _context = new SIGEBIContext(options);
            _prestamosServices = new PrestamosServices(IprestamosRepository.Object, loggerMock.Object, createvalidator, updatevalidator, cacheservice.Object);
        }

        [Fact]
        public async Task SavePrestamo_Check_If_BookExist()
        {
            // Arrange
            PrestamoCreateDto dtoPrestamo = new PrestamoCreateDto() 
            { IdLibros = 1234556431243};

            // Act
            var libro = await _prestamosServices.CreatePrestamoAsync(dtoPrestamo);
            string message = "Libro not found.";

            // Assert
            Assert.IsType<OperationResult>(libro);
            Assert.Equal(message, libro.Message);
            Assert.False(libro.Success);
        }
    }
}