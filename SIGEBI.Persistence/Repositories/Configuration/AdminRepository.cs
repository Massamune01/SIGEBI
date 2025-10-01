using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Security.Configuration.ValidarAdmin;
using SIGEBI.Persistence.Security.Configuration.ValidarBibliot;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public sealed class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(SIGEBIContext context, ILogger<AdminRepository> logger) : base(context)
        {
            _logger = logger;
        }

        public List<Admin> GetAdminById(int id)
        {
            try
            {
                _logger.LogInformation("Searching Admin by Id {Id}", id);
                return _context.Admins
                               .Where(b => b.Id == id)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching bibliotecario by Id");
                return new List<Admin>();
            }
        }

        public List<Admin> GetAdminByName(string name)
        {
            try
            {
                _logger.LogInformation("Searching Admins for nombre that contains {Name}", name);
                return _context.Admins
                               .Where(b => b.Nombre.Contains(name) || b.Apellido.Contains(name))
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Admins for nombre");
                return new List<Admin>();
            }
        }

        public List<Admin> GetAdminByRol(int rol)
        {
            try
            {
                _logger.LogInformation("Searching Admin by rol {Rol}", rol);
                return _context.Admins
                               .Where(b => b.RolId == rol)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Admin by rol");
                return new List<Admin>();
            }
        }

        public List<Admin> GetAdminByStatus(Status status)
        {
            try
            {
                _logger.LogInformation("Searching Admins with status {Status}.", status);
                return _context.Admins
                               .Where(b => b.AdminEstatus == status)
                               .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Admins for status");
                return new List<Admin>();
            }
        }

        public override async Task<OperationResult> Save(Admin entity)
        {
            var result = new OperationResult();

            try
            {
                var validator = new ValidarAdmin();

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("The validation is not valid.");
                    return result; // Se detiene si no es válido
                }

                _logger.LogInformation("Saving Admin entity.");
                await base.Save(entity);
                await _context.SaveChangesAsync();
                result.Data = entity;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Admin entity");
                result.Success = false;
                result.Message = "Error saving Admin entity.";
            }

            return result;
        }

        public override async Task<OperationResult> Remove(Admin entity)
        {
            var result = new OperationResult();
            try
            {
                _logger.LogInformation("Removing Admin entity");
                result = await base.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing Admin entity");
                result.Success = false;
                result.Message = "Error removing Admin entity.";
            }
            return result;
        }

        public override async Task<OperationResult> Update(Admin entity)
        {
            var result = new OperationResult();
            try
            {
                var validator = new ValidarAdmin();

                var validationResult = await validator.ValidateAsync(entity);

                if (!validationResult.IsValid)
                {
                    result.Success = false;
                    result.Message = string.Join("The validation is not valid.");
                    return result;
                }

                _logger.LogInformation("Updating Admin entity.");
                result = await base.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Admin entity");
                result.Success = false;
                result.Message = "Error updating Admin entity.";
            }
            return result;
        }
    }
}
