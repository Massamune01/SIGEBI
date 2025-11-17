using FluentValidation;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Persistence.Security.Configuration.ValidarBibliot
{
    public class ValidarBibliotecario
    {
        public OperationResult ValidateBibliotecario(Bibliotecarios biblio)
        {
            var result = new OperationResult();

            // --- Objeto no nulo ---
            if (biblio == null)
                return new OperationResult { Success = false, Message = "El objeto Bibliotecarios no puede ser nulo." };

            // --- Nombre: obligatorio, máximo 60 caracteres ---
            if (string.IsNullOrWhiteSpace(biblio.Nombre))
                return new OperationResult { Success = false, Message = "El nombre es obligatorio." };
            if (biblio.Nombre.Length > 60)
                return new OperationResult { Success = false, Message = "El nombre no puede exceder 60 caracteres." };

            // --- Apellido: opcional, máximo 80 caracteres ---
            if (!string.IsNullOrWhiteSpace(biblio.Apellido) && biblio.Apellido.Length > 80)
                return new OperationResult { Success = false, Message = "El apellido no puede exceder 80 caracteres." };


            // --- Email: obligatorio, formato y longitud ---
            if (string.IsNullOrWhiteSpace(biblio.Email))
                return new OperationResult { Success = false, Message = "El correo electrónico es obligatorio." };
            if (!biblio.Email.Contains("@"))
                return new OperationResult { Success = false, Message = "El correo electrónico no tiene un formato válido." };
            if (biblio.Email.Length > 80)
                return new OperationResult { Success = false, Message = "El correo electrónico no puede exceder 80 caracteres." };

            // --- Totales: deben ser >= 0 ---
            if (biblio.TotalDevoluciones < 0)
                return new OperationResult { Success = false, Message = "TotalDevoluciones debe ser mayor o igual a 0." };
            if (biblio.TotalHorasTrabajadas < 0)
                return new OperationResult { Success = false, Message = "TotalHorasTrabajadas debe ser mayor o igual a 0." };
            if (biblio.TotalClientesAtendidos < 0)
                return new OperationResult { Success = false, Message = "TotalClientesAtendidos debe ser mayor o igual a 0." };
            if (biblio.TotalPrestamos < 0)
                return new OperationResult { Success = false, Message = "TotalPrestamos debe ser mayor o igual a 0." };

            // --- StatusBiblio: obligatorio y válido ---
            if (!Enum.IsDefined(typeof(Status), biblio.BiblioEstatus))
                return new OperationResult { Success = false, Message = "El estado del bibliotecario no es válido." };

            // --- IdLgOpBiblio: opcional, si se especifica debe ser >= 0 ---
            if (biblio.IdLgOpBiblio.HasValue && biblio.IdLgOpBiblio.Value < 0)
                return new OperationResult { Success = false, Message = "IdLgOpBiblio debe ser mayor o igual a 0 si se proporciona." };

            result.Success = true;
            result.Message = "Validación exitosa.";
            return result;
        }
    }
}
