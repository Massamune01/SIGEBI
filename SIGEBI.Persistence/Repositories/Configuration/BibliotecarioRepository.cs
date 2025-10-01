using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Dtos.Configuration.LibroDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Security.Configuration.ValidarBibliot;
using SIGEBI.Persistence.Security.Configuration.ValidarLibro;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public sealed class BibliotecarioRepository : BaseRepository<Bibliotecarios>, IBibliotecariosRepository

    {
        private readonly ILogger<BibliotecarioRepository> _logger;

        public BibliotecarioRepository(SIGEBIContext context, ILogger<BibliotecarioRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public List<Bibliotecarios> GetAdminByStatus(Status status)
        {
            try
            {
                _logger.LogInformation("Searching Bibliotecarios with status {Status}.", status);
                return _context.Bibliotecarios
                               .Where(b => b.BiblioEstatus == status)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching bibliotecarios for status");
                return new List<Bibliotecarios>();
            }
        }

        public List<Bibliotecarios> GetBiblioById(int id)
        {
            try
            {
                _logger.LogInformation("Searching bibliotecario with Id {Id}", id);
                return _context.Bibliotecarios
                               .Where(b => b.Id == id)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching bibliotecario with Id");
                return new List<Bibliotecarios>();
            }
        }

        public List<Bibliotecarios> GetBiblioByName(string name)
        {
            try
            {
                _logger.LogInformation("Searching Bibliotecarios for nombre that contains {Name}", name);
                return _context.Bibliotecarios
                               .Where(b => b.Nombre.Contains(name) || b.Apellido.Contains(name))
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Bibliotecarios for nombre");
                return new List<Bibliotecarios>();
            }
        }

        public List<Bibliotecarios> GetBiblioByRol(int rol)
        {
            try
            {
                _logger.LogInformation("Buscando bibliotecarios con rol {Rol}", rol);
                return _context.Bibliotecarios
                               .Where(b => b.RolId == rol)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo bibliotecarios por rol");
                return new List<Bibliotecarios>();
            }
        }

        public override async Task<OperationResult> Save(Bibliotecarios entity)
        {
            var result = new OperationResult();

            try
            {
                var validator = new ValidarBibliot();

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("The validation is not valid.");
                    return result; // Se detiene si no es válido
                }

                _logger.LogInformation("Saving Bibliotecario entity.");
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

        public override async Task<OperationResult> Remove(Bibliotecarios entity)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Removing Bibliotecario entity");
                result = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing Bibliotecario entity");
                result.Success = false;
                result.Message = "Error removing Bibliotecario entity.";
            }
            return result;
        }

        public override async Task<OperationResult> Update(Bibliotecarios entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarBibliot();

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("The validation is not valid.");
                    return result;
                }

                _logger.LogInformation("Updating Bibliotecario entity.");
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
