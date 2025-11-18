using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Security.Configuration.ValidarLibro;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public sealed class LibroRepository : BaseRepository<Libro>, ILibrosRepository
    {
        private readonly ILogger<LibroRepository> _logger;
        public LibroRepository(SIGEBIContext context, ILogger<LibroRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<List<Libro>> GetLibroByAutor(string autor)
        {
            if (string.IsNullOrWhiteSpace(autor))
                return new List<Libro>();

            return await _entities
                .Where(l => EF.Functions.Like(l.autor, $"%{autor}%") && l.Status == Status.Activo)
                .ToListAsync();
        }

        public async Task<List<Libro>> GetLibroByCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
                return new List<Libro>();

            
            return await _entities
                .Where(l => l.categoria == categoria && l.Status == Status.Activo)
                .ToListAsync();
        }

        public async Task<List<Libro>> GetLibroByEditorial(string editorial)
        {
            return await _context.Set<Libro>()
            .Where(l => l.editorial == editorial)
            .ToListAsync();
        }

        public async Task<Libro?> GetLibroById(Int64 isbn)
        {
            return await _context.Set<Libro>()
            .FirstOrDefaultAsync(l => l.ISBN == isbn);
            
        }

        public async Task<List<Libro>> GetLibroByTitulo(string titulo)
        {
            return await _context.Set<Libro>()
            .Where(l => l.titulo == titulo)
            .ToListAsync();
        }

        //Guarda y loguea errores
        public override async Task<OperationResult> Save(Libro entity)
        {
            var result = new OperationResult();

            try 
            {
                var validator = new ValidarLibro();

                var validationResult = validator.ValidateLibro(entity);

                if (!validationResult.Success)
                {
                    result.Success = false;
                    result.Message = validationResult.Message;
                    return result;
                }

                _logger.LogInformation("Saving Libro entity with ISBN: {ISBN}", entity.ISBN);
                _logger.LogInformation("Valores del libro: " +
                $"ISBN:{entity.ISBN}, Año:{entity.anoPublicacion}, " +
                $"Cantidad:{entity.cantidad}, NumPaginas:{entity.numPaginas}, " +
                $"Status:{entity.Status}, Op:{entity.IdLgOpLibro}");
                await base.Save(entity);
                await _context.SaveChangesAsync();
                result.Data = entity;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Libro entity");
                result.Success = false;
                result.Message = "Error saving Libro entity.";
            }

            return result;
        }

        public override async Task<bool> Exists(Expression<Func<Libro, bool>> filter)
        {
            _logger.LogInformation("Checking existence of Libro entity with given filter");
            return await base.Exists(filter);
        }

        override public async Task<OperationResult> Remove(Libro entity)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Removing Libro entity with ISBN: {ISBN}", entity.ISBN);
                result = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing Libro entity");
                result.Success = false;
                result.Message = "Error removing Libro entity.";
            }
            return result;
        }

        override public async Task<OperationResult> GetAll()
        {
            _logger.LogInformation("Retrieving all Libro entities");
            return await base.GetAll();
        }

        public override Task<OperationResult> GetAll(Expression<Func<Libro, bool>> filter)
        {
            _logger.LogInformation("Retrieving Libro entities with given filter");
            return base.GetAll(filter);
        }

        override public async Task<OperationResult> GetEntityBy(int Id)
        {
            _logger.LogInformation("Retrieving Libro entity with ISBN: {ISBN}", Id);
            return await base.GetEntityBy(Id);
        }

        override public async Task<OperationResult> Update(Libro entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarLibro();

                var validationResult = validator.ValidateLibro(entity);

                if (!validationResult.Success)
                {
                    result.Success = false;
                    result.Message = validationResult.Message;
                    return result;
                }

                _logger.LogInformation("Updating Libro entity with ISBN: {ISBN}", entity.ISBN);
                result = await base.Update(entity);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Libro entity");
                result.Success = false;
                result.Message = "Error updating Libro entity.";
            }
            return result;
        }
    }
}
