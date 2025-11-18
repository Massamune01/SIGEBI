using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Services;
using SIGEBI.Application.Validators.Base;
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
        private readonly IMapper _mapper;

        public PrestamoServicesTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            var loggerMock = new Mock<ILogger<PrestamosServices>>();
            var IprestamosRepository = new Mock<IPrestamosRepository>();
            var validator = new Mock<IValidatorBase<PrestamoDto>>();
            var cacheservice = new Mock<ICacheService>();
            var mapper = new Mock<IMapper>();
            _mapper = mapper.Object;
            _context = new SIGEBIContext(options);
            _prestamosServices = new PrestamosServices(IprestamosRepository.Object, loggerMock.Object, validator.Object, cacheservice.Object, _mapper);
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