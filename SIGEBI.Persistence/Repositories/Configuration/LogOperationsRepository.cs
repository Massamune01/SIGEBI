using System.Linq.Expressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Dtos.Configuration.LogOperationsDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Security.Configuration.ValidadLogOp;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public sealed class LogOperationsRepository : BaseRepository<LogOperations>, ILogOperationsRepository
    {
        private readonly ILogger<LogOperationsRepository> _logger;
        public LogOperationsRepository(SIGEBIContext context, ILogger<LogOperationsRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public Task<bool> Exists(Expression<Func<LogOperations, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetAll(Expression<Func<LogOperations, bool>> filter)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Retrieving LogOperation entities with filter");

                // Podemos ejecutar la consulta aquí para aplicar ordenamiento y evitar castear después
                result.Data = await _entities.Where(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving filtered LogOperation entities");
                result.Success = false;
                result.Message = $"Error retrieving filtered log operations: {ex.Message}";
            }
            return result;
        }

        public async Task<List<LogOperations>> GetByEntity(string entity)
        {
            _logger.LogInformation("Retrieving log operations for entity: {Entity}", entity);
            return await _entities
                .Where(l => l.Entity == entity)
                .ToListAsync();
        }

        public async Task<List<LogOperations>> GetByStatus(string statusOp)
        {
            _logger.LogInformation("Retrieving log operations with status: {StatusOp}", statusOp);
            return await _entities
                .Where(l => l.StatusOp == statusOp)
                .ToListAsync();

        }

        public async Task<List<LogOperations>> GetByTypeOperation(string typeOperation)
        {
            _logger.LogInformation("Retrieving log operations for type: {TypeOperation}", typeOperation);
            return await _entities
                .Where(l => l.TypeOperation == typeOperation)
                .ToListAsync();
        }

        public async Task<OperationResult> Remove(LogOperations entity)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Removing LogOperation for entity: {Entity}", entity.Entity);
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
                result.Message = "LogOperation removed successfully";
                result.Data = entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing LogOperation");
                result.Success = false;
                result.Message = "Error removing LogOperation";
            }
            return result;
        }

        public async Task<OperationResult> Save(LogOperations entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarLogOperations();

                var validationResult = validator.Validate(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return result; // Se detiene si no es válido
                }

                _logger.LogInformation("Saving LogOperation for entity: {Entity}", entity.Entity);
                await base.Save(entity);
                result.Data = entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving LogOperation");
                result.Success = false;
                result.Message = "Error saving LogOperation";
            }
            return result;
        }

        public async Task<OperationResult> Update(LogOperations entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarLogOperations();

                var validationResult = validator.Validate(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return result; // Se detiene si no es válido
                }
                _logger.LogInformation("Updating LogOperation for entity: {Entity}", entity.Entity);
                await base.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating LogOperation");
                result.Success = false;
                result.Message = "Error updating LogOperation";
            }
            return result;
        }
    }
}
