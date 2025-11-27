using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Validators.Configuration.PrestamosValidators;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Application
{
    public class PrestamoServicesTest
    {
        private readonly IPrestamoFacade _prestamoFacade;
        private readonly SIGEBIContext _context;

        public PrestamoServicesTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;


            _context = new SIGEBIContext(options);
            var loggerMock = new Mock<ILogger<PrestamoFacade>>();
            var loggerMock1 = new Mock<ILogger<PrestamoValidator>>();
            var loggerMock2 = new Mock<ILogger<PrestamosRepository>>();
            var repository = new PrestamosRepository(_context,loggerMock2.Object);
            var validator = new PrestamoValidator(repository, loggerMock1.Object);
            var mapper = new Mock<IMapper>();
            _prestamoFacade = new PrestamoFacade(repository, validator, loggerMock.Object, mapper.Object);
        }

        [Fact]
        public async Task SavePrestamo_Check_If_BookExist()
        {
            // Arrange
            PrestamoCreateDto dtoPrestamo = new PrestamoCreateDto() 
            { IdLibros = 1234556431243, DatePrest = DateTime.Now};

            // Act
            var libro = await _prestamoFacade.CreatePrestamoAsync(dtoPrestamo);
            string message = "Libro not found.";

            // Assert
            Assert.IsType<ServiceResult>(libro);
            Assert.Equal(message, libro.Message);
            Assert.False(libro.Success);
        }
    }
}