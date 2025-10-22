using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Services
{
    public class AdminServices : IAdminService
    {
        private readonly ILogger<AdminServices> _logger;
        private readonly IAdminRepository _adminRepository;
        private readonly IValidatorBase<AdminCreateDto> _createValidator;
        private readonly IValidatorBase<AdminUpdateDto> _updateValidator;

        public AdminServices(IAdminRepository adminRepository, ILogger<AdminServices> logger, 
            IValidatorBase<AdminCreateDto> createValidator, IValidatorBase<AdminUpdateDto> updateValidator)
        {
            _logger = logger;
            _adminRepository = adminRepository;
            _createValidator = createValidator;
            _updateValidator = updateValidator;

        }

        public async Task<ServiceResult> CreateAdminAsync(AdminCreateDto adminCreateDto)
        {
            ServiceResult result = new ServiceResult();

            _logger.LogInformation("Creating an admin");
            try
            {
                //Validaciones de negocio
                var adminvalidation =await _createValidator.ValidateCreate(adminCreateDto);
                if (!adminvalidation.IsValid) 
                { 
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", adminvalidation.Errors);
                    return result;
                }

                _logger.LogInformation("Validating admin data");

                Admin admin = new Admin()
                {
                    Nombre = adminCreateDto.Nombre,
                    Apellido = adminCreateDto.Apellido,
                    Edad = adminCreateDto.Edad,
                    Genero = adminCreateDto.Genero,
                    Email = adminCreateDto.Email,
                    Nacimiento = adminCreateDto.Nacimiento,
                    RolId = adminCreateDto.RolId,
                    AdminEstatus = (adminCreateDto.AdminEstatus ?? Status.Activo),
                    IdLgOpAdmin = adminCreateDto.IdLgOpAdmin
                };

                var oResultAdmin = await _adminRepository.Save(admin);

                if(oResultAdmin is null)
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
            catch(Exception ex)
            { 
                result.Success = false;
                result.Message = "An error occurred while creating the admin.";
                _logger.LogError(ex, result.Message);
                return result;

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

                if(existingAdminResult is null)
                {
                    _logger.LogWarning("Admin not found with ID: {AdminId}", id);
                    result.Success = false;
                    result.Message = "Admin not found.";
                    return result;
                }

                var admin = (Admin?)existingAdminResult.Data;

                var deleteResult = await _adminRepository.Remove(admin);

                if(!deleteResult.Success || deleteResult.Data == null)
                {
                    result.Success = false;
                    result.Message = "Failed to delete the admin.";
                    return result;
                }

                result.Success = true;
                result.Message = "Admin deleted successfully.";
                _logger.LogInformation(result.Message);
                
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the admin.";
                _logger.LogError(ex, result.Message);
                return result;
            }
            return result;
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
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the admin.";
                _logger.LogError(ex, result.Message);
                return result;
            }
            return result;
        }

        public async Task<ServiceResult> GetAllAdminAsync()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving all admins");
                var existingAdminsResult =  _adminRepository.GetAll();
                if (existingAdminsResult is null)
                {
                    _logger.LogWarning("No admins found.");
                    result.Success = false;
                    result.Message = "No admins found.";
                    return result;
                }
                result.Success = true;
                result.Data = existingAdminsResult.Result.Data;
                result.Message = "Admins retrieved successfully.";
                _logger.LogInformation(result.Message);
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving admins.";
                _logger.LogError(ex, result.Message);
                return result;
            }
            return result;
        }

        public async Task<ServiceResult> UpdateAdminAsync(AdminUpdateDto adminUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Validaciones de negocio
                var adminvalidation = await _updateValidator.ValidateUpdate(adminUpdateDto);
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
                var oResultAdmin = await _adminRepository.GetEntityBy(adminUpdateDto.Id);
                if(oResultAdmin is null)
                {
                    _logger.LogWarning("Admin not found with ID: {AdminId}", adminUpdateDto.Id);
                    result.Success = false;
                    result.Message = "Admin not found.";
                    return result;
                }
                Admin admin = new Admin()
                {
                    Id = adminUpdateDto.Id,
                    Nombre = adminUpdateDto.Nombre,
                    Apellido = adminUpdateDto.Apellido,
                    Edad = adminUpdateDto.Edad,
                    Genero = adminUpdateDto.Genero,
                    Email = adminUpdateDto.Email,
                    Nacimiento = adminUpdateDto.Nacimiento,
                    RolId = adminUpdateDto.RolId,
                    AdminEstatus = (adminUpdateDto.AdminEstatus ?? Status.Activo),
                    IdLgOpAdmin = adminUpdateDto.IdLgOpAdmin
                };

                var updateResult = await _adminRepository.Update(admin);

                if (!updateResult.Success || updateResult.Data == null)
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
            catch(Exception ex)
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
