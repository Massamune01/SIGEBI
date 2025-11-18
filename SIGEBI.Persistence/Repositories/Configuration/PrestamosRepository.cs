using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Dtos.Configuration.PrestamosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Entities.Configuration.Prestamos;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Security.Configuration.ValidarLibro;
using SIGEBI.Persistence.Security.Configuration.ValidarPrestamos;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public class PrestamosRepository : BaseRepository<Prestamos>, IPrestamosRepository
    {
        private readonly ILogger<PrestamosRepository> _logger;
        public PrestamosRepository(SIGEBIContext context, ILogger<PrestamosRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<OperationResult> GetLibroWithTituloAndISBN()
        {
            var result = new OperationResult();

            var libros = _context.Libro
                    .Select(l => new LibroDto
                    {
                        ISBN = l.ISBN,
                        titulo = l.titulo
                        })
                    .ToList();

            if (libros == null)
            {
                result.Success = false;
                result.Message = "No books found.";
                _logger.LogWarning("No books found in the database.");
                return result;
            }

            result.Data = libros;
            result.Success = true;
            return result;
        }

        // Get Libro by IdLibro to check availability by column cantidad
        public async Task<OperationResult> GetLibroForPrestamoAsync(Int64 IdLibro)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Retrieving Libro with Id {IdLibro} for Prestamo check", IdLibro);
                var libro = await _context.Libro
                    .FirstOrDefaultAsync(l => l.ISBN == IdLibro);
                if (libro == null)
                {
                    result.Success = false;
                    result.Message = "Libro not found.";
                    _logger.LogWarning("Libro with Id {IdLibro} not found", IdLibro);
                    return result;
                }
                else if(libro.ISBN <= 0)
                {
                    result.Success = false;
                    result.Message = "Libro with Id 0 or low cannot be find";
                    _logger.LogWarning("Libro with Id {IdLibro} not found", IdLibro);
                    return result;
                }

                if (libro.cantidad <= 0)
                {
                    result.Success = false;
                    result.Message = "Libro is not available for loan.";
                    _logger.LogWarning("Libro with Id {IdLibro} is not available for loan", IdLibro);
                    return result;
                }
                result.Data = libro;
                result.Success = true;
                result.Message = "Libro is available for loan.";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving Libro for Prestamo check");
                result.Success = false;
                result.Message = "Error retrieving Libro for Prestamo check";
                return result;
            }
        }

        public async Task<List<Prestamos>> GetPrestamosByClienteId(int clienteId)
        {
            return _context.Prestamos
                       .Where(p => p.IdCliente == clienteId)
                       .ToList();
        }

        public async Task<Prestamos?> GetPrestamosById(int id)
        {
            _logger.LogInformation("Buscando préstamo con Id {Id}", id);

            try
            {
                _logger.LogInformation("Searching Prestamo with Id {Id}", id);
                var prestamo = await _context.Prestamos
                               .Where(p => p.Id == id)
                               .ToListAsync();


                return prestamo.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Cliente with Id");
                return new Prestamos();
            }
        }

        public override async Task<OperationResult> Save(Prestamos entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarPrestamos();

                var validationResult = validator.ValidatePrestamo(entity);

                if (!validationResult.Success)
                {
                    result.Success = false;
                    result.Message = validationResult.Message;
                    return result;
                }

                _logger.LogInformation("Saving Prestamo for a client");
                await base.Save(entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving the Prestamos for the cliente");
                result.Success = false;
                result.Message = "Error saving the Prestamos for the cliente";
            }
            return result;
        }

        public override async Task<OperationResult> Remove(Prestamos entity)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Removing Prestamo for a Client");
                result = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Removing Prestamo entity.");
                result.Success = false;
                result.Message = "Error Removing Prestamo entity.";
            }
            return result;
        }

        override public async Task<OperationResult> GetAll()
        {
            _logger.LogInformation("Retrieving all Prestamos entities");
            return await base.GetAll();
        }

        public override Task<OperationResult> GetAll(Expression<Func<Prestamos, bool>> filter)
        {
            _logger.LogInformation("Retrieving Prestamos entities with given filter");
            return base.GetAll(filter);
        }

        override public async Task<OperationResult> GetEntityBy(int Id)
        {
            _logger.LogInformation("Retrieving Prestamos entity with ISBN: {ISBN}", Id);
            return await base.GetEntityBy(Id);
        }

        override public async Task<OperationResult> Update(Prestamos entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarPrestamos();

                var validationResult = validator.ValidatePrestamo(entity);

                if (!validationResult.Success)
                {
                    result.Success = false; 
                    result.Message = validationResult.Message;
                    return result;
                }

                _logger.LogInformation("Updating Prestamo entity.");
                result = await base.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Updating Prestamo entity.");
                result.Success = false;
                result.Message = "Error Updating Prestamo entity.";
            }
            return result;
        }


    }
}
