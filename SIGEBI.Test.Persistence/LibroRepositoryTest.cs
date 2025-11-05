using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Persistence
{
    public class LibroRepositoryTest
    {
        private readonly LibroRepository _libroRepository;
        private readonly SIGEBIContext _context;
        private readonly Mock<ILogger<LibroRepository>> _loggerMock;

        public LibroRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase("SIGEBI")
                .Options;

            _context = new SIGEBIContext(options);
            _loggerMock = new Mock<ILogger<LibroRepository>>();
            _libroRepository = new LibroRepository(_context, _loggerMock.Object);
        }

        [Fact]
        public async Task SaveLibro_When_Libro_IsNull()
        {
            // Arrange
            Libro libro = null;
            string message = "El objeto Libro no puede ser nulo.";

            // Act
            var result = await _libroRepository.Save(libro);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_ISBN_Length_IsInvalid()
        {
            var libro = new Libro() { ISBN = 12345 };
            string message = "El ISBN debe tener 10 o 13 dígitos.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_Titulo_IsEmpty()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = ""
            };
            string message = "El campo título no puede estar vacío.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_Titulo_IsTooLong()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = new string('A', 101)
            };
            string message = "El campo título no puede exceder los 100 caracteres.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_Autor_IsEmpty()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = "Libro válido",
                autor = ""
            };
            string message = "El campo autor no puede estar vacío.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_Editorial_IsEmpty()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = "Libro válido",
                autor = "Autor",
                editorial = ""
            };
            string message = "El campo editorial no puede estar vacío.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_AnoPublicacion_IsInvalid()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = "Libro válido",
                autor = "Autor",
                editorial = "Editorial",
                anoPublicacion = -1
            };
            string message = "El campo año de publicación debe ser un número positivo.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_Categoria_IsEmpty()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = "Libro válido",
                autor = "Autor",
                editorial = "Editorial",
                anoPublicacion = 2020,
                categoria = ""
            };
            string message = "El campo categoría no puede estar vacío.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_NumPaginas_IsInvalid()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = "Libro válido",
                autor = "Autor",
                editorial = "Editorial",
                anoPublicacion = 2020,
                categoria = "Ficción",
                numPaginas = -1
            };
            string message = "El campo número de páginas debe ser un número positivo.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task SaveLibro_When_Cantidad_IsNegative()
        {
            var libro = new Libro()
            {
                ISBN = 1234567890,
                titulo = "Libro válido",
                autor = "Autor",
                editorial = "Editorial",
                anoPublicacion = 2020,
                categoria = "Ficción",
                numPaginas = 200,
                cantidad = -1
            };
            string message = "El campo cantidad debe ser un número positivo.";

            var result = await _libroRepository.Save(libro);

            Assert.False(result.Success);
            Assert.Equal(message, result.Message);
        }
    }
}
