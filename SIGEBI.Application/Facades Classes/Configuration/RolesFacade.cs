using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.RolDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Facades_Classes.Configuration
{
    public class RolesFacade : IRolesFacade
    {
        private readonly IRolRepository _rolRepository;
        private readonly IValidatorBase<RolDto> _Validator;
        private readonly ILogger<RolesFacade> _logger;

        public RolesFacade(IRolRepository rolRepository, 
            IValidatorBase<RolDto> validator, ILogger<RolesFacade> logger)
        {
            _rolRepository = rolRepository;
            _Validator = validator;
            _logger = logger;
        }

        public async Task<ServiceResult> CreateRol(RolCreateDto createRolDto)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                _logger.LogInformation("Validating role creation data for role: {RolName}", createRolDto.Rol);

                RolDto rolDto = new RolDto
                {
                    Rol = createRolDto.Rol
                };

                var validationResult = await _Validator.Validate(rolDto, 1);
                if (validationResult.Errors.Any())
                {
                    result.Success = false;
                    result.Message = "Role creation validation failed.";
                    result.Data = validationResult.Errors;
                    return result;
                }

                _logger.LogInformation("Creating a role with name: {RolName}", createRolDto.Rol);

                if (createRolDto is null)
                {
                    result.Success = false;
                    result.Message = "The role data cannot be null.";
                    return result;
                }

                Roles rol = new Roles()
                {
                    Rol = createRolDto.Rol,
                    IdLgOpRol = createRolDto.IdLgOpRol
                };

                var existingRol = await _rolRepository.Save(rol);
                if(!existingRol.Success)
                {
                    result.Success = false;
                    result.Message = existingRol.Message;
                    _logger.LogError($"Error: {result.Message}");
                    return result;
                }

                result.Success = true;
                result.Data = existingRol;
                result.Message = "Role created successfully.";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the role.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetEntityBy(int id)
        {
            ServiceResult result = new ServiceResult();

            try
            {
                _logger.LogInformation("Retrieving role with ID: {RolId}", id);
                var oResultGetEntity = await _rolRepository.GetEntityBy(id);
                if (oResultGetEntity is null)
                {
                    result.Success = false;
                    result.Message = "Role not found.";
                    return result;
                }
                else
                {
                    result.Success = true;
                    result.Message = "Rol found.";
                    result.Data = oResultGetEntity.Data;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the role.";
                _logger.LogError(ex, result.Message);
            }

            return result;
        }

        public async Task<ServiceResult> GetRolAll()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Retrieving all roles.");
                var oResultGetAll = await _rolRepository.GetAll();
                if (oResultGetAll is null)
                {
                    result.Success = false;
                    result.Message = "No roles found.";
                    return result;
                }
                result.Success = true;
                result.Data = oResultGetAll.Data;
                result.Message = oResultGetAll.Message;
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving roles.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> RemoveRol(RolRemoveDto removeRolDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Removing role with ID: {RolId}", removeRolDto.Id);
                if (removeRolDto is null)
                {
                    result.Success = false;
                    result.Message = "The role data cannot be null.";
                    return result;
                }
                var ResultRol = await _rolRepository.GetEntityBy(removeRolDto.Id);
                if (ResultRol is null)
                {
                    result.Success = false;
                    result.Message = "Role not found.";
                    return result;
                }

                Roles rol = new Roles()
                {
                    Id = removeRolDto.Id,
                    RolEstatus = Domain.Enums.Status.Inactivo,
                    IdLgOpRol = removeRolDto.IdLgOpLibro
                };

                var updateResult = await _rolRepository.Remove(rol);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = updateResult.Message;
                    return result;
                }
                else
                {
                    result.Success = true;
                    result.Message = "Role remove succesfuly";
                    result.Data = ResultRol.Data;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while removing the role.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> UpdateRol(RolUpdateDto updateRolDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation("Validating role update data for role ID: {RolId}", updateRolDto.Id);
                RolDto rolDto = new RolDto
                {
                    Id = updateRolDto.Id,
                    Rol = updateRolDto.Rol
                };

                var validationResult = await _Validator.Validate(rolDto, 2);
                if (validationResult.Errors.Any())
                {
                    result.Success = false;
                    result.Message = "Role update validation failed.";
                    result.Data = validationResult.Errors;
                    return result;
                }

                _logger.LogInformation("Updating role with ID: {RolId}", updateRolDto.Id);
                if (updateRolDto is null)
                {
                    result.Success = false;
                    result.Message = "The role data cannot be null.";
                    return result;
                }
                var ResultRol = await _rolRepository.GetEntityBy(updateRolDto.Id);
                if (ResultRol is null)
                {
                    result.Success = false;
                    result.Message = "Role not found.";
                    return result;
                }
                Roles rol = new Roles()
                {
                    Id = updateRolDto.Id,
                    Rol = updateRolDto.Rol
                };
                var updateResult = await _rolRepository.Update(rol);
                if (!updateResult.Success)
                {
                    result.Success = false;
                    result.Message = "Failed to update the role.";
                    return result;
                }
                else
                {
                    result.Success = true;
                    result.Message = "Update succesfuly.";
                    result.Data = ResultRol.Data;
                }

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the role.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }
    }
}
