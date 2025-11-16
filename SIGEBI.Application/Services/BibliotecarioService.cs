using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.AdminDtos;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Application.Services
{
    public class BibliotecarioService : IBibliotecarioService
    {
        private readonly ILogger<BibliotecarioService> _logger;
        private readonly IBibliotecariosRepository _bibliotecariosRepository;
        private readonly IValidatorBase<BibliotecarioDto> _Validator;
        private readonly ICacheService _cacheService;

        public BibliotecarioService(ILogger<BibliotecarioService> logger, IBibliotecariosRepository bibliotecariosRepository, 
            IValidatorBase<BibliotecarioDto> validator, ICacheService cacheService)
        {
            _logger = logger;
            _bibliotecariosRepository = bibliotecariosRepository;
            _Validator = validator;
            _cacheService = cacheService;
        }

        public async Task<ServiceResult> CreateBibliotecarioAsync(BibliotecarioCreateDto bibliotecarioCreateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Validaciones de negocio
                _logger.LogInformation("Validating bibliotecario");
                BibliotecarioDto bibliotecarioDto = new BibliotecarioDto()
                {
                    Nombre = bibliotecarioCreateDto.Nombre,
                    Apellido = bibliotecarioCreateDto.Apellido,
                    Edad = bibliotecarioCreateDto.Edad,
                    Genero = bibliotecarioCreateDto.Genero,
                    Email = bibliotecarioCreateDto.Email,
                    Nacimiento = bibliotecarioCreateDto.Nacimiento,
                    RolId = bibliotecarioCreateDto.RolId,
                    TotalDevoluciones = bibliotecarioCreateDto.TotalDevoluciones ?? 0,
                    TotalHorasTrabajadas = bibliotecarioCreateDto.TotalHorasTrabajadas ?? 0,
                    TotalClientesAtendidos = bibliotecarioCreateDto.TotalClientesAtendidos ?? 0,
                    TotalPrestamos = bibliotecarioCreateDto.TotalPrestamos ?? 0,
                };

                var createvalidator = await _Validator.Validate(bibliotecarioDto,1);
                if (!createvalidator.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", createvalidator.Errors);
                    return result;
                }


                _logger.LogInformation("Creating a bibliotecario");
                if (bibliotecarioCreateDto is null)
                {
                    result.Success = false;
                    result.Message = "The bibliotecario data cannot be null.";
                    return result;
                }
                Bibliotecarios newBibliotecario = new Bibliotecarios()
                {
                    Nombre = bibliotecarioCreateDto.Nombre,
                    Apellido = bibliotecarioCreateDto.Apellido,
                    Edad = bibliotecarioCreateDto.Edad,
                    Genero = bibliotecarioCreateDto.Genero,
                    Email = bibliotecarioCreateDto.Email,
                    Nacimiento = bibliotecarioCreateDto.Nacimiento,
                    RolId = bibliotecarioCreateDto.RolId,
                    TotalDevoluciones = bibliotecarioCreateDto.TotalDevoluciones ?? 0,
                    TotalHorasTrabajadas = bibliotecarioCreateDto.TotalHorasTrabajadas ?? 0,
                    TotalClientesAtendidos = bibliotecarioCreateDto.TotalClientesAtendidos ?? 0,
                    TotalPrestamos = bibliotecarioCreateDto.TotalPrestamos ?? 0,
                    BiblioEstatus = bibliotecarioCreateDto.BiblioEstatus ?? Domain.Enums.Status.Activo,
                    IdLgOpBiblio = bibliotecarioCreateDto.IdLgOpBiblio
                };
                var createdBibliotecario = await _bibliotecariosRepository.Save(newBibliotecario);
                if (createdBibliotecario is null)
                {
                    result.Success = false;
                    result.Message = "Failed to create bibliotecario.";
                    return result;
                }
                result.Success = true;
                result.Data = createdBibliotecario;
                result.Message = "Bibliotecario created successfully.";
                _logger.LogInformation(result.Message);
                _cacheService.ClearKeys();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while creating the bibliotecario.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> DeleteBibliotecarioAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation($"Deleting bibliotecario with ID: {id}");
                var existingBibliotecarioResult = await _bibliotecariosRepository.GetBiblioById(id);
                if (existingBibliotecarioResult is null)
                {
                    result.Success = false;
                    result.Message = "Bibliotecario not found.";
                    return result;
                }

                var oBibliotecarioResult = (Bibliotecarios?)existingBibliotecarioResult.FirstOrDefault();

                var deleteResult = await _bibliotecariosRepository.Remove(oBibliotecarioResult);
                if (!deleteResult.Success)
                {
                    result.Success = false;
                    result.Message = "Failed to delete the bibliotecario.";
                    return result;
                }
                result.Success = true;
                result.Message = "Bibliotecario deleted successfully.";
                _logger.LogInformation(result.Message);
                _cacheService.ClearKeys();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while deleting the bibliotecario.";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public async Task<ServiceResult> GetAllBibliotecariosAsync()
        {
            ServiceResult result = new ServiceResult();
            const string cacheKey = "ALL_Bibliotecario";

            _logger.LogInformation("Verifying existing cache with Key {cacheKey}", cacheKey);
            if (_cacheService.TryGet(cacheKey, out List<BibliotecarioDto> list))
            {
                result.Success = true;
                result.Data = list;
                result.Message = "Bibliotecarios retrieved from cache.";
                return result;
            }
            
            try
            {
                _logger.LogInformation("Retrieving all bibliotecarios");
                var bibliotecarios = await _bibliotecariosRepository.GetAll();
                if (bibliotecarios is null)
                {
                    result.Success = false;
                    result.Message = "No bibliotecarios found.";
                    return result;
                }
                
                _cacheService.Set(cacheKey, bibliotecarios.Data);

                result.Success = true;
                result.Data = bibliotecarios.Data;
                result.Message = "Bibliotecarios retrieved successfully.";
                _logger.LogInformation(result.Message);
                
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving bibliotecarios.";
                _logger.LogError(ex, result.Message);
                return result;
            }
            return result;
        }

        public async Task<ServiceResult> GetBibliotecarioByIdAsync(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                _logger.LogInformation($"Retrieving bibliotecario with ID: {id}");
                var existingBibliotecarioResult =  await _bibliotecariosRepository.GetBiblioById(id);
                if (existingBibliotecarioResult is null || !existingBibliotecarioResult.Any())
                {
                    result.Success = false;
                    result.Message = "Bibliotecario not found.";
                    return result;
                }
                result.Success = true;
                result.Data = existingBibliotecarioResult.FirstOrDefault();
                result.Message = "Bibliotecario retrieved successfully.";
                _logger.LogInformation(result.Message);
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while retrieving the bibliotecario.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }

        public async Task<ServiceResult> UpdateBibliotecarioAsync(BibliotecarioUpdateDto bibliotecarioUpdateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                //Validaciones de negocio
                _logger.LogInformation("Validating bibliotecario");
                BibliotecarioDto bibliotecarioDto = new BibliotecarioDto()
                {
                    Nombre = bibliotecarioUpdateDto.Nombre,
                    Apellido = bibliotecarioUpdateDto.Apellido,
                    Edad = bibliotecarioUpdateDto.Edad,
                    Genero = bibliotecarioUpdateDto.Genero,
                    Email = bibliotecarioUpdateDto.Email,
                    Nacimiento = bibliotecarioUpdateDto.Nacimiento,
                    RolId = bibliotecarioUpdateDto.RolId,
                    TotalDevoluciones = bibliotecarioUpdateDto.TotalDevoluciones ?? 0,
                    TotalHorasTrabajadas = bibliotecarioUpdateDto.TotalHorasTrabajadas ?? 0,
                    TotalClientesAtendidos = bibliotecarioUpdateDto.TotalClientesAtendidos ?? 0,
                    TotalPrestamos = bibliotecarioUpdateDto.TotalPrestamos ?? 0,
                };

                var updatevalidator = await _Validator.Validate(bibliotecarioDto,2);
                if (!updatevalidator.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", updatevalidator.Errors);
                    return result;
                }

                _logger.LogInformation($"Updating bibliotecario with ID: {bibliotecarioUpdateDto.Id}");
                var existingBibliotecarioResult = await _bibliotecariosRepository.GetBiblioById(bibliotecarioUpdateDto.Id);
                if (existingBibliotecarioResult is null || !existingBibliotecarioResult.Any())
                {
                    result.Success = false;
                    result.Message = "Bibliotecario not found.";
                    return result;
                }
                Bibliotecarios oBibliotecarioResult = new Bibliotecarios()
                {
                    Id = bibliotecarioUpdateDto.Id,
                    Nombre = bibliotecarioUpdateDto.Nombre,
                    Apellido = bibliotecarioUpdateDto.Apellido,
                    Edad = bibliotecarioUpdateDto.Edad,
                    Genero = bibliotecarioUpdateDto.Genero,
                    Email = bibliotecarioUpdateDto.Email,
                    Nacimiento = bibliotecarioUpdateDto.Nacimiento,
                    RolId = bibliotecarioUpdateDto.RolId,
                    TotalDevoluciones = bibliotecarioUpdateDto.TotalDevoluciones ?? 0,
                    TotalHorasTrabajadas = bibliotecarioUpdateDto.TotalHorasTrabajadas ?? 0,
                    TotalClientesAtendidos = bibliotecarioUpdateDto.TotalClientesAtendidos ?? 0,
                    TotalPrestamos = bibliotecarioUpdateDto.TotalPrestamos ?? 0,
                    BiblioEstatus = bibliotecarioUpdateDto.BiblioEstatus ?? Domain.Enums.Status.Activo,
                    IdLgOpBiblio = bibliotecarioUpdateDto.IdLgOpBiblio
                };

                var updateResult = await _bibliotecariosRepository.Update(oBibliotecarioResult);

                if (!updateResult.Success || updateResult.Data is null)
                {
                    result.Success = false;
                    result.Message = "Failed to update the bibliotecario.";
                    return result;
                }
                result.Success = true;
                result.Data = updateResult.Data;
                result.Message = "Bibliotecario updated successfully.";
                _logger.LogInformation(result.Message);
                _cacheService.ClearKeys();
                return result;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "An error occurred while updating the bibliotecario.";
                _logger.LogError(ex, result.Message);
                return result;
            }
        }
    }
}
