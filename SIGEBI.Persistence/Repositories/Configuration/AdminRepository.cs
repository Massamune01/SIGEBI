using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;
using SIGEBI.Persistence.Base;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Security.Configuration.ValidarAdmin;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public sealed class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        private readonly ILogger<AdminRepository> _logger;

        public AdminRepository(SIGEBIContext context, ILogger<AdminRepository> logger) : base(context)
        {
            _logger = logger;
        }
        public AdminRepository(SIGEBIContext context) : base(context)
        {
        }
        
        public async Task<List<Admin>> GetAdminByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Searching Admin by Id {Id}", id);
                return await _context.Admin
                               .Where(b => b.Id == id)
                               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching bibliotecario by Id");
                return new List<Admin>();
            }
        }

        public async Task<List<Admin>> GetAdminByEmailAsync(string email)
        {
            try
            {
                _logger.LogInformation("Searching Admins for email that contains {email}", email);
                return await _context.Admin
                    .Where(a =>  a.Email == email)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error searching Admins for email");
                return new List<Admin>();
            }
        }

        public async Task<List<Admin>> GetAdminByCedulaAsync(string cedula)
        {
            try
            {
                _logger.LogInformation("Searching Admins for cedula that contains {Cedula}", cedula);
                return await _context.Admin
                    .Where(b => b.Cedula.Contains(cedula))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Admins for cedula");
                return new List<Admin>();
            }
        }

        public async Task<List<Admin>> GetAdminByNameAsync(string name)
        {
            try
            {
                _logger.LogInformation("Searching Admins for nombre that contains {Name}", name);
                return await _context.Admin
                               .Where(b => b.Nombre.Contains(name) || b.Apellido.Contains(name))
                               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Admins for nombre");
                return new List<Admin>();
            }
        }

        public async Task<List<Admin>> GetAdminByRolAsync(int rol)
        {
            try
            {
                _logger.LogInformation("Searching Admin by rol {Rol}", rol);
                return await _context.Admin
                               .Where(b => b.RolId == rol)
                               .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching Admin by rol");
                return new List<Admin>();
            }
        }

        public async Task<List<Admin>> GetAdminByStatusAsync(Status status)
        {
            try
            {
                _logger.LogInformation("Searching Admins with status {Status}.", status);
                return await _context.Admin
                               .Where(b => b.AdminEstatus == status)
                               .ToListAsync();
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

                var validationResult = validator.ValidateAdmin(entity);

                if (!validationResult.Success)
                {
                    result.Success = false;
                    result.Message = validationResult.Message;
                    return result;
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

                var validationResult = validator.ValidateAdmin(entity);

                if (!validationResult.Success)
                {
                    result.Success = false;
                    result.Message = validationResult.Message;
                    return result;
                }

                _logger.LogInformation("Updating Admin entity.");
                result = await base.Update(entity);
                await _context.SaveChangesAsync();

                result.Success = true;
                result.Message = "Admin Updated Succesfuly";
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Admin entity");
                result.Success = false;
                result.Message = "Error updating Admin entity."; 
                return result;
            }
           
        }
    }
}
