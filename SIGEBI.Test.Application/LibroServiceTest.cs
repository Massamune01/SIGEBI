using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Facades_Classes.Configuration;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Validators.Configuration.LibroValidators;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Application
{
    public class LibroServiceTest
    {
        private readonly ILibroFacade _libroFacade;
        private readonly SIGEBIContext _context;

        public LibroServiceTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            _context = new SIGEBIContext(options);
            var loggerMock = new Mock<ILogger<LibroFacade>>();
            var loggerMock1 = new Mock<ILogger<LibroValidator>>();
            var loggerMock2 = new Mock<ILogger<LibroRepository>>();
            var repository = new LibroRepository(_context, loggerMock2.Object);
            var validator = new LibroValidator(repository, loggerMock1.Object);
            var mapper = new Mock<IMapper>();
            _libroFacade = new LibroFacade(repository, validator, loggerMock.Object);
        }

        [Fact]
        //Check that ISBN is required
        public async Task SaveLibro_Check_If_ISBN_Is_Required()
        {
            // Arrange
            var dtoLibro = new LibroCreateDto
            {
                titulo = "Cien Años de Soledad",
                autor = "Gabriel Garcia Marquez",
                ISBN = 0,
                anoPublicacion = 2025
            };
            // Act
            var result = await _libroFacade.CreateLibroAsync(dtoLibro);
            string message = "ISBN is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task SaveLibro_Check_If_Titulo_Is_Required()
        {
            // Arrange
            var dtoLibro = new LibroCreateDto
            {
                titulo = "",
                autor = "Gabriel Garcia Marquez",
                ISBN = 1012000000009,
                anoPublicacion = 2025
            };
            // Act
            var result = await _libroFacade.CreateLibroAsync(dtoLibro);
            string message = "Titulo is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that Autor is required
        public async Task SaveLibro_Check_If_Autor_Is_Required()
        {
            // Arrange
            var dtoLibro = new LibroCreateDto
            {
                titulo = "Cien Años de Soledad",
                autor = "",
                ISBN = 1012000000009,
                anoPublicacion = 2025
            };
            // Act
            var result = await _libroFacade.CreateLibroAsync(dtoLibro);
            string message = "Autor is required.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

        [Fact]
        //Check that cantidad is non-negative
        public async Task SaveLibro_Check_If_Cantidad_Is_Non_Negative()
        {
            // Arrange
            var dtoLibro = new LibroCreateDto
            {
                titulo = "Cien Años de Soledad",
                autor = "Gabriel Garcia Marquez",
                ISBN = 1012000000009,
                cantidad = -5,
                anoPublicacion = 2025
            };
            // Act
            var result = await _libroFacade.CreateLibroAsync(dtoLibro);
            string message = "Cantidad cannot be negative.";
            // Assert
            Assert.IsType<ServiceResult>(result);
            Assert.Equal(message, result.Message);
            Assert.False(result.Success);
        }

    }
}

