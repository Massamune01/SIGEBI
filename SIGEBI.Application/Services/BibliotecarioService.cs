﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos;
using SIGEBI.Application.Interfaces;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Application.Validators.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Application.Services
{
    public class BibliotecarioService : IBibliotecarioService
    {
        private readonly ILogger<BibliotecarioService> _logger;
        private readonly IBibliotecariosRepository _bibliotecariosRepository;
        private readonly IValidatorBase<BibliotecarioCreateDto> _createValidator;
        private readonly IValidatorBase<BibliotecarioUpdateDto> _updateValidator;

        public BibliotecarioService(ILogger<BibliotecarioService> logger, IBibliotecariosRepository bibliotecariosRepository, IValidatorBase<BibliotecarioUpdateDto> updateValidator, IValidatorBase<BibliotecarioCreateDto> createvalidator)
        {
            _logger = logger;
            _bibliotecariosRepository = bibliotecariosRepository;
            _updateValidator = updateValidator;
            _createValidator = createvalidator;
        }

        public async Task<ServiceResult> CreateBibliotecarioAsync(BibliotecarioCreateDto bibliotecarioCreateDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                // Business validations can be added here
                var bibliotecarioValidation = _createValidator.ValidateCreate(bibliotecarioCreateDto);
                /* Todavia no se implementa la validacion
                if (!bibliotecarioValidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", adminvalidation.Errors);
                    return result;
                }
                */

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
                    Nacimiento = bibliotecarioCreateDto.Nacimiento ?? DateTime.Now,
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
                result.Success = true;
                result.Data = bibliotecarios;
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
                var bibliotecarioValidation = _updateValidator.ValidateUpdate(bibliotecarioUpdateDto);
                /* Todavia no se implementa la validacion
                if (!bibliotecarioValidation.IsValid)
                {
                    result.Success = false;
                    result.Message = "Validation errors: " + string.Join(", ", adminvalidation.Errors);
                    return result;
                }
                */

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
                    Nacimiento = bibliotecarioUpdateDto.Nacimiento ?? DateTime.Now,
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
