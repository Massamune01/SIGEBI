﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    internal class PrestamosRepository : BaseRepository<Prestamos>, IPrestamosRepository
    {
        private readonly ILogger<PrestamosRepository> _logger;
        public PrestamosRepository(SIGEBIContext context, ILogger<PrestamosRepository> logger) : base(context)
        {
            _logger = logger;
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

            return await _context.Prestamos
                .FirstOrDefaultAsync(p => p.Id == id && p.PrestamosStatus.ToString() == "Disponible");
        }

        public override async Task<OperationResult> Save(Prestamos entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarPrestamos();

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return result; // Se detiene si no es válido
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

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
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
