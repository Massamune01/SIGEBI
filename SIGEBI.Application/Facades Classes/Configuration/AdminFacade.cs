using System.ComponentModel.DataAnnotations;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Facades_Classes.Configuration
{
    public class AdminFacade : IAdminFacade
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IValidatorBase<AdminDto> _adminValidator;
        private readonly ILogger<AdminFacade> _logger;

        public AdminFacade(IAdminRepository adminRepository, IValidatorBase<AdminDto> adminValidator
            , ILogger<AdminFacade> logger)
        {
            _adminRepository = adminRepository;
            _adminValidator = adminValidator;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateAdminAsync(AdminCreateDto adminCreateDto)
        {
            _logger.LogInformation("Creating an admin");
            ServiceResult result = new ServiceResult();
            try
            {
                AdminDto adminDto = new AdminDto()
                {
                    Nombre = adminCreateDto.Nombre,
                    Apellido = adminCreateDto.Apellido,
                    Cedula = adminCreateDto.Cedula,
                    Edad = adminCreateDto.Edad,
                    Genero = adminCreateDto.Genero,
                    Email = adminCreateDto.Email,
                    Nacimiento = adminCreateDto.Nacimiento
                };

                _logger.LogInformation("validating a admin");
                var adminvalidation = await _adminValidator.Validate(adminDto, 1);

                if (!adminvalidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", adminvalidation.Errors);
                    return result;
                }

                Admin admin = new Admin()
                {
                    Nombre = adminCreateDto.Nombre,
                    Apellido = adminCreateDto.Apellido,
                    Edad = adminCreateDto.Edad,
                    Cedula = adminCreateDto.Cedula,
                    Genero = adminCreateDto.Genero,
                    Email = adminCreateDto.Email,
                    Nacimiento = adminCreateDto.Nacimiento,
                    RolId = 3
                };

                var oResultAdmin = await _adminRepository.Save(admin);

                if (oResultAdmin is null)
                {
                    result.Success = false;
                    result.Message = "Failed to create admin.";
                    return result;
                }
                else
                {
                    result.Success = true;
                    result.Data = oResultAdmin;
                    result.Message = "Admin created successfully.";
                    _logger.LogInformation(result.Message);
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the admin: " + ex.Message;
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> DeleteAdminAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Deleting an admin");
                var existingAdminResult = await _adminRepository.GetEntityBy(id);

                if (existingAdminResult is null)
                {
                    _logger.LogWarning("Admin not found with ID: {AdminId}", id);
                    result.Success = false;
                    result.Message = "Admin not found.";
                    return result;
                }

                var admin = (Admin?)existingAdminResult.Data;

                var deleteResult = await _adminRepository.Remove(admin);

                if (!deleteResult.Success || deleteResult.Data == null)
                {
                    result.Success = false;
                    result.Message = "Failed to delete the admin.";
                    return result;
                }

                result.Success = true;
                result.Message = "Admin deleted successfully.";
                _logger.LogInformation(result.Message);
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the admin.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetAdminByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving admin with ID: {AdminId}", id);
                var existingAdminResult = await _adminRepository.GetEntityBy(id);
                if (existingAdminResult is null)
                {
                    _logger.LogWarning("Admin not found with ID: {AdminId}", id);
                    result.Success = false;
                    result.Message = "Admin not found.";
                    return result;
                }
                result.Success = true;
                result.Data = existingAdminResult.Data;
                result.Message = "Admin retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the admin: " + ex.Message;
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> GetAllAdminAsync()
        {
            ServiceResult result = new ServiceResult();

            try
            {


                _logger.LogInformation("Retrieving all admins");
                var existingAdminsResult = await _adminRepository.GetAll();
                if (existingAdminsResult.Data is null)
                {
                    _logger.LogWarning("No admins found.");
                    result.Success = false;
                    result.Message = "No admins found.";
                    return result;
                }

                List<Admin> admins = (List<Admin>)existingAdminsResult.Data;

                result.Success = true;
                result.Data = admins;
                result.Message = "Admins retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving admins.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> UpdateAdminAsync(AdminUpdateDto adminUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Validaciones de negocio

                AdminDto adminDto = new AdminDto()
                {
                    Nombre = adminUpdateDto.Nombre,
                    Apellido = adminUpdateDto.Apellido,
                    Cedula = adminUpdateDto.Cedula,
                    Edad = adminUpdateDto.Edad,
                    Genero = adminUpdateDto.Genero,
                    Email = adminUpdateDto.Email,
                    Nacimiento = adminUpdateDto.Nacimiento,
                    RolId = adminUpdateDto.RolId,
                    IdLgOpAdmin = adminUpdateDto.IdLgOpAdmin,
                    AdminEstatus = adminUpdateDto.AdminEstatus

                };

                var adminvalidation = await _adminValidator.Validate(adminDto, 2);
                if (!adminvalidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", adminvalidation.Errors);
                    return result;
                }


                _logger.LogInformation("Updating an admin with ID: {AdminId}", adminUpdateDto.Id);
                if (adminUpdateDto is null)
                {
                    result.Success = false;
                    result.Message = "The admin data cannot be null.";
                    return result;
                }

                Admin admin = new Admin()
                {
                    Id = adminUpdateDto.Id,
                    Nombre = adminUpdateDto.Nombre,
                    Apellido = adminUpdateDto.Apellido,
                    Edad = adminUpdateDto.Edad,
                    Cedula = adminUpdateDto.Cedula,
                    Genero = adminUpdateDto.Genero,
                    Email = adminUpdateDto.Email,
                    Nacimiento = adminUpdateDto.Nacimiento,
                    RolId = adminUpdateDto.RolId,
                    AdminEstatus = adminUpdateDto.AdminEstatus,
                    IdLgOpAdmin = adminUpdateDto.IdLgOpAdmin
                };

                var updateResult = await _adminRepository.Update(admin);

                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = "Failed to update the admin.";
                    return result;
                }
                result.Success = true;
                result.Data = updateResult.Data;
                result.Message = "Admin updated successfully.";
                _logger.LogInformation(result.Message);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the admin.";
                _logger.LogError(ex, result.Message);
                return result;
            }
            return result;
        }
    }
}
