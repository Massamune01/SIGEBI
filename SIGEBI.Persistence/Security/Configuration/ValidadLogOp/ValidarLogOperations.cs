using System;
using System.Collections.Generic;
using SIGEBI.Domain.Base;


namespace SIGEBI.Persistence.Security.Configuration.ValidadLogOp
{
    public sealed class ValidarLogOperations
    {
        public OperationResult ValidateLogOperation(LogOperations log)
        {
            var result = new OperationResult();

            if (log == null)
            {
                result.Success = false;
                result.Message = "El objeto LogOperation no puede ser nulo.";
                return result;
            }

            // Entity obligatorio y máximo 90 caracteres
            if (string.IsNullOrWhiteSpace(log.Entity))
            {
                result.Success = false;
                result.Message = "El campo Entity no puede estar vacío.";
                return result;
            }

            if (log.Entity.Length > 90)
            {
                result.Success = false;
                result.Message = "El campo Entity no puede exceder los 90 caracteres.";
                return result;
            }

            // Fecha obligatoria y no en el futuro
            if (log.Fecha == default)
            {
                result.Success = false;
                result.Message = "La fecha es obligatoria.";
                return result;
            }

            if (log.Fecha > DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha no puede estar en el futuro.";
                return result;
            }

            // TypeOperation obligatorio y máximo 100 caracteres
            if (string.IsNullOrWhiteSpace(log.TypeOperation))
            {
                result.Success = false;
                result.Message = "El campo TypeOperation no puede estar vacío.";
                return result;
            }

            if (log.TypeOperation.Length > 100)
            {
                result.Success = false;
                result.Message = "El campo TypeOperation no puede exceder los 100 caracteres.";
                return result;
            }

            // Descripción opcional pero máximo 100 caracteres
            if (!string.IsNullOrEmpty(log.Descripcion) && log.Descripcion.Length > 100)
            {
                result.Success = false;
                result.Message = "La descripción no puede exceder los 100 caracteres.";
                return result;
            }

            // LastUpdateBy y UpdateBy opcionales, pero deben ser fechas válidas si se proporcionan
            if (log.LastUpdateBy.HasValue && log.LastUpdateBy > DateTime.Now)
            {
                result.Success = false;
                result.Message = "LastUpdateBy no puede estar en el futuro.";
                return result;
            }

            if (log.UpdateBy.HasValue && log.UpdateBy > DateTime.Now)
            {
                result.Success = false;
                result.Message = "UpdateBy no puede estar en el futuro.";
                return result;
            }

            result.Success = true;
            return result;
        }
    }
}
