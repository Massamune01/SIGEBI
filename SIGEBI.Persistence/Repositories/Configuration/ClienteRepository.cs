using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Dtos.Configuration.ClienteDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Security.Configuration.ValidarBibliot;
using SIGEBI.Persistence.Security.Configuration.ValidarCliente;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public sealed class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        private readonly ILogger<ClienteRepository> _logger;

        public ClienteRepository(SIGEBIContext context, ILogger<ClienteRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public List<Cliente> GetClienteById(int id)
        {
            try
            {
                _logger.LogInformation("Searching Clientes with Id {Id}", id);
                return _context.Clientes
                               .Where(b => b.Id == id)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Cliente with Id");
                return new List<Cliente>();
            }
        }

        public List<Cliente> GetClienteByName(string name)
        {
            try
            {
                _logger.LogInformation("Searching Clientes for nombre that contains {Name}", name);
                return _context.Clientes
                               .Where(b => b.Nombre.Contains(name) || b.Apellido.Contains(name))
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Cliente for nombre");
                return new List<Cliente>();
            }
        }

        public override async Task<OperationResult> Save(Cliente entity)
        {
            var result = new OperationResult();

            try
            {
                var validator = new ValidarCliente();

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("The validation is not valid.");
                    return result; // Se detiene si no es válido
                }

                _logger.LogInformation("Saving Cliente entity.");
                await base.Save(entity);
                await _context.SaveChangesAsync();
                result.Data = entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Cliente entity");
                result.Success = false;
                result.Message = "Error saving Cliente entity.";
            }
            return result;
        }

        public override async Task<OperationResult> Remove(Cliente entity)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Removing Cliente entity");
                result = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing Cliente entity");
                result.Success = false;
                result.Message = "Error removing Cliente entity.";
            }
            return result;
        }

        public override async Task<OperationResult> Update(Cliente entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarCliente();

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("The validation is not valid.");
                    return result;
                }

                _logger.LogInformation("Updating Cliente entity.");
                result = await base.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Cliente entity");
                result.Success = false;
                result.Message = "Error updating Cliente entity.";
            }
            return result;
        }
    }
}
