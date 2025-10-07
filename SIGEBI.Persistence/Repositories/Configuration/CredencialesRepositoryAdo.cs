using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using SIGEBI.Application.Repositories.Configuration;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Repository;
using SIGEBI.Infraestructure.Data.Configuration;
using SIGEBI.Persistence.Models.Configuration.Credenciales;

namespace SIGEBI.Persistence.Repositories.Configuration
{
    public class CredencialesRepositoryAdo : ICredencialesRepository
    {
        private readonly ILogger<CredencialesRepositoryAdo> _logger;
        private readonly HelperDb _dbHelper;

        public CredencialesRepositoryAdo(HelperDb helperDb, ILogger<CredencialesRepositoryAdo> logger)
        {
            _dbHelper = helperDb;
            _logger = logger;
        }

        public async Task<bool> Exists(Expression<Func<Credenciales, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetAll()
        {
            var result = new OperationResult();
            try
            {
                var credentials = await _dbHelper.ExecuteReaderAsync(
                    "SP_GET_ALL_CREDENCIALES",
                    reader => new CredencialesGetModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                        Usuario = reader.GetString(reader.GetOrdinal("Usuario")),
                        PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                    }
                );

                result.Success = true;
                result.Data = credentials;
                result.Message = credentials.Any() ? "Credenciales obtenidas correctamente." : "No se encontraron credenciales.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener credenciales");
                result.Success = false;
                result.Message = "Error al obtener credenciales";
            }

            return result;
        }

        public async Task<OperationResult> GetAll(Expression<Func<Credenciales, bool>> filter)
        {
            throw new NotImplementedException();

        }

        public Task<OperationResult> GetCredencialesByClienteId(int clienteId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> GetCredencialesById(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id }
                };

                var credential = await _dbHelper.ExecuteReaderAsync(
                    "SP_GET_CRE_BY_ID",
                    reader => new CredencialesGetModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                        Usuario = reader.GetString(reader.GetOrdinal("Usuario")),
                        PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                    },
                    parameters
                );
                if (credential != null)
                {
                    operationResult.Success = true;
                    operationResult.Data = credential;
                    operationResult.Message = "Credencial obtenida correctamente.";
                    return operationResult;
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró la credencial.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al obtener credencial por Id";
                _logger.LogError(ex, operationResult.Message);
            }
            return operationResult;
        }

        public async Task<OperationResult> GetEntityBy(int Id)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id }
                };

                var credential = await _dbHelper.ExecuteReaderAsync(
                    "SP_GET_CRE_BY_ID",
                    reader => new CredencialesGetModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId")),
                        Usuario = reader.GetString(reader.GetOrdinal("Usuario")),
                        PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash"))
                    },
                    parameters
                );
                if (credential != null)
                {
                    operationResult.Success = true;
                    operationResult.Data = credential;
                    operationResult.Message = "Credencial obtenida correctamente.";
                    return operationResult;
                }
                else
                {
                    operationResult.Success = false;
                    operationResult.Message = "No se encontró la credencial.";
                }
            }
            catch (Exception ex)
            {
                operationResult.Success = false;
                operationResult.Message = "Error al obtener credencial por Id";
                _logger.LogError(ex, operationResult.Message);
            }
            return operationResult;
        }

        public async Task<OperationResult> Remove(Credenciales entity)
        {
            var result = new OperationResult();
            try
            {
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id }
            };

                var rows = await _dbHelper.ExecuteNonQueryAsync(
                    "SP_REMOVE_CREDENCIAL",
                    parameters
                );

                result.Success = rows > 0;
                result.Message = rows > 0 ? "Credencial eliminada correctamente." : "No se encontró la credencial a eliminar.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar credencial");
                result.Success = false;
                result.Message = "Error al eliminar credencial";
            }

            return result;
        }

        public async Task<OperationResult> Save(Credenciales entity)
        {
            var result = new OperationResult();
            try
            {
                var parameters = new List<SqlParameter>
                {
                new SqlParameter("@ClienteId", SqlDbType.Int) { Value = entity.ClienteId },
                new SqlParameter("@Usuario", SqlDbType.NVarChar, 80) { Value = entity.Usuario },
                new SqlParameter("@PasswordHash", SqlDbType.NVarChar, 200) { Value = entity.PasswordHash }

                };

                var rows = await _dbHelper.ExecuteNonQueryAsync(
                    "SP_INSERT_CREDENCIAL",
                    parameters
                );

                result.Success = rows > 0;
                result.Message = rows > 0 ? "Credencial guardada correctamente." : "No se pudo guardar la credencial.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al guardar credencial");
                result.Success = false;
                result.Message = "Error al guardar credencial";
            }

            return result;
        }

        public async Task<OperationResult> Update(Credenciales entity)
        {
            var result = new OperationResult();

            try
            {
                // Creamos los parámetros según la tabla real
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = entity.Id },
                    new SqlParameter("@Usuario", SqlDbType.NVarChar, 80) { Value = entity.Usuario }
                };
                var rows = await _dbHelper.ExecuteNonQueryAsync(
                    "SP_UPDATE_CREDENCIAL_USER",
                    parameters
                );

                if (rows > 0)
                {
                    result.Success = true;
                    result.Message = "Credencial actualizada correctamente.";
                }

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar credencial");
                result.Success = false;
                result.Message = "Error al actualizar credencial";
            }

            return result;
        }
    }
}
