using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Infraestructure.Data.Configuration;
using SIGEBI.Persistence.Models.Configuration.Rol;
using SIGEBI.Persistence.Security.Configuration.ValidarLibro;
using SIGEBI.Persistence.Security.Configuration.ValidarRol;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public class RolRepositoryAdo : IRolRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<RolRepositoryAdo> _logger;
        private readonly HelperDb  _dbHelper;

        public RolRepositoryAdo(HelperDb HelperDb, ILogger<RolRepositoryAdo> logger)
        {
            _logger = logger;
            _dbHelper = HelperDb;
        }

        public Task<bool> Exists(Expression<Func<Roles, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetAll()
        {
            var result = new OperationResult();

            try
            {
                var roles = await _dbHelper.ExecuteReaderAsync(
                    "SP_GET_ALL_ROLES",
                    reader => new RolGetModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Rol = reader.GetString(reader.GetOrdinal("Rol")),
                        RolEstatus = (Domain.Enums.Status)reader.GetInt32(reader.GetOrdinal("StatusRol")),
                        IdLgOpLibro = reader.IsDBNull(reader.GetOrdinal("IdLgOpRol"))
                            ? null
                            : reader.GetInt32(reader.GetOrdinal("IdLgOpRol"))
                    }
                );

                if (roles == null || roles.Count == 0)
                {
                    result.Success = true;
                    result.Message = "No se encontraron roles.";
                    result.Data = new List<RolGetModel>();
                }
                else
                {
                    result.Success = true;
                    result.Message = "Roles obtenidos correctamente.";
                    result.Data = roles;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los roles");
                result.Success = false;
                result.Message = "Error al obtener todos los roles";
            }

            return result;
        }

        public Task<OperationResult> GetAll(Expression<Func<Roles, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id }
                };

                var rol = await _dbHelper.ExecuteReaderAsync(
                    "SP_GET_ROL_BY_ID",
                    reader => new RolGetModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Rol = reader.GetString(reader.GetOrdinal("Rol")),
                        RolEstatus = (Domain.Enums.Status)reader.GetInt32(reader.GetOrdinal("StatusRol")),
                        IdLgOpLibro = reader.IsDBNull(reader.GetOrdinal("IdLgOpRol"))
                            ? null
                            : reader.GetInt32(reader.GetOrdinal("IdLgOpRol"))
                    },parameters
                );
                if (rol == null)
                {
                    result.Success = false;
                    result.Message = "No se encontró el rol con el Id proporcionado.";
                }
                else
                {
                    result.Success = true;
                    result.Message = "Rol obtenido correctamente.";
                    result.Data = rol;
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener rol por Id";
                _logger.LogError(ex, result.Message);
            }
            return result;
        }

        public Roles? GetRolById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Roles>> GetRolByName(string name)
        {
            OperationResult result = new OperationResult();
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Rol", SqlDbType.NVarChar, 100) { Value = name }
                };
                var roles =  _dbHelper.ExecuteReaderAsync(
                    "SP_GET_ROL_BY_NAME",
                    reader => new Roles
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Rol = reader.GetString(reader.GetOrdinal("Rol")),
                        RolEstatus = (Domain.Enums.Status)reader.GetInt32(reader.GetOrdinal("StatusRol")),
                        IdLgOpRol = reader.IsDBNull(reader.GetOrdinal("IdLgOpRol"))
                            ? null
                            : reader.GetInt32(reader.GetOrdinal("IdLgOpRol"))
                    },parameters
                ).Result;
                return roles;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = "Error al obtener rol por nombre";
                _logger.LogError(ex, result.Message);
                return new List<Roles>();
            }
        }

        public async Task<OperationResult> Remove(Roles entity)
        {
            var result = new OperationResult();

            try
            {
                    
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id }
                };

                
                var rows = await _dbHelper.ExecuteNonQueryAsync(
                    "SP_SOFT_DELETE_ROL_BY_ID",
                    parameters
                );

                result.Success = rows > 0;
                result.Message = rows > 0
                    ? "Rol eliminado correctamente."
                    : "No se encontró el rol a eliminar.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar rol");
                result.Success = false;
                result.Message = "Error al eliminar rol";
            }

            return result;
        }

        public async Task<OperationResult> Save(Roles entity)
        {
            var result = new OperationResult();

            var validator = new ValidarRol();

            var validationResult = await validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
            {
                result.Success = false;
                result.Message = string.Join("The validation is not valid");
                return result;
            }

            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Rol", SqlDbType.NVarChar, 100) { Value = entity.Rol },
                    new SqlParameter("@RolEstatus", SqlDbType.Int) { Value = (int)entity.RolEstatus }
                };

                var rows = await _dbHelper.ExecuteNonQueryAsync(
                    "SP_INSERT_ROL",
                    parameters
                );

                result.Success = rows > 0;
                result.Message = rows > 0
                    ? "Rol guardado correctamente."
                    : "No se pudo guardar el rol.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar rol");
                result.Success = false;
                result.Message = "Error al guardar rol";
            }

            return result;
        }

        public async Task<OperationResult> Update(Roles entity)
        {
            var result = new OperationResult();

            var validator = new ValidarRol();

            var validationResult = await validator.ValidateAsync(entity);

            if (!validationResult.IsValid)
            {
                result.Success = false;
                result.Message = string.Join("The validation is not valid");
                return result;
            }

            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
                    new SqlParameter("@Rol", SqlDbType.NVarChar, 100) { Value = entity.Rol },
                    new SqlParameter("@RolEstatus", SqlDbType.Int) { Value = (int)entity.RolEstatus }
                };

                var rows = await _dbHelper.ExecuteNonQueryAsync(
                    "SP_UPDATE_ROL",
                    parameters
                );

                result.Success = rows > 0;
                result.Message = rows > 0
                    ? "Rol actualizado correctamente."
                    : "No se pudo actualizar el rol.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar rol");
                result.Success = false;
                result.Message = "Error al actualizar rol";
            }
            return result;
        }
    }
}
