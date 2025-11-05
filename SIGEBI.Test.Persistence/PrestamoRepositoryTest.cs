using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Repositories.Configuration;

namespace SIGEBI.Test.Persistence
{
    public class PrestamoRepositoryTest
    {
        private readonly IPrestamosRepository _repository;
        private readonly SIGEBIContext _context;

        public PrestamoRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<SIGEBIContext>()
                .UseInMemoryDatabase(databaseName: "TestPrestamoDB")
                .Options;

            _context = new SIGEBIContext(options);
            var mockLogger = new Mock<ILogger<PrestamosRepository>>();
            _repository = new PrestamosRepository(_context, mockLogger.Object);
        }

        [Fact]
        public async Task SavePrestamo_When_Entity_IsNull_ShouldFail()
        {
            var result = await _repository.Save(null!);

            Assert.False(result.Success);
            Assert.Contains("no puede ser nulo", result.Message);
        }

        [Fact]
        public async Task SavePrestamo_When_DatePrest_IsEmpty_ShouldFail()
        {
            var prestamo = new Prestamos
            {
                DatePrest = default
            };

            var result = await _repository.Save(prestamo);

            Assert.False(result.Success);
            Assert.Contains("fecha de préstamo es obligatoria", result.Message);
        }

        [Fact]
        public async Task SavePrestamo_When_DatePrest_InFuture_ShouldFail()
        {
            var prestamo = new Prestamos
            {
                DatePrest = DateTime.Now.AddDays(3),
                DateDevol = DateTime.Now.AddDays(5)
            };

            var result = await _repository.Save(prestamo);

            Assert.False(result.Success);
            Assert.Contains("no puede estar en el futuro", result.Message);
        }

        [Fact]
        public async Task SavePrestamo_When_DateDevol_IsEmpty_ShouldFail()
        {
            var prestamo = new Prestamos
            {
                DatePrest = DateTime.Now,
                DateDevol = default
            };

            var result = await _repository.Save(prestamo);

            Assert.False(result.Success);
            Assert.Contains("fecha de devolución es obligatoria", result.Message);
        }

        [Fact]
        public async Task SavePrestamo_When_DateDevol_Before_DatePrest_ShouldFail()
        {
            var prestamo = new Prestamos
            {
                DatePrest = DateTime.Now,
                DateDevol = DateTime.Now.AddDays(-1)
            };

            var result = await _repository.Save(prestamo);

            Assert.False(result.Success);
            Assert.Contains("debe ser posterior", result.Message);
        }

        [Fact]
        public async Task SavePrestamo_When_DateWasDevol_Before_DatePrest_ShouldFail()
        {
            var prestamo = new Prestamos
            {
                DatePrest = DateTime.Now,
                DateDevol = DateTime.Now.AddDays(5),
                DateWasDevol = DateTime.Now.AddDays(-2)
            };

            var result = await _repository.Save(prestamo);

            Assert.False(result.Success);
            Assert.Contains("no puede ser anterior", result.Message);
        }

        [Fact]
        public async Task SavePrestamo_When_IdLibros_IsInvalid_ShouldFail()
        {
            var prestamo = new Prestamos
            {
                DatePrest = DateTime.Now,
                DateDevol = DateTime.Now.AddDays(3),
                IdLibros = 0,
                IdCliente = 1
            };

            var result = await _repository.Save(prestamo);

            Assert.False(result.Success);
            Assert.Contains("Debe especificar un libro válido", result.Message);
        }

        [Fact]
        public async Task SavePrestamo_When_IdCliente_IsInvalid_ShouldFail()
        {
            var prestamo = new Prestamos
            {
                DatePrest = DateTime.Now,
                DateDevol = DateTime.Now.AddDays(3),
                IdLibros = 1,
                IdCliente = 0
            };

            var result = await _repository.Save(prestamo);

            Assert.False(result.Success);
            Assert.Contains("Debe especificar un cliente válido", result.Message);
        }
    }
}
