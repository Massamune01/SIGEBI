using FluentValidation;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Persistence.Security.Configuration.ValidarAdmin
{
    public class ValidarAdmin
    {
        public OperationResult ValidateAdmin(Admin admin)
        {
            OperationResult result = new OperationResult();

            // --- Validación de objeto ---
            if (admin == null)
            { 
                result.Success = false;
                result.Message = "El objeto Admin no puede ser nulo.";
                return result; 
            }

            // --- Nombre: obligatorio, máximo 60 caracteres ---
            if (string.IsNullOrWhiteSpace(admin.Nombre))
            { 
                result.Success = false;
                result.Message = "El nombre es obligatorio.";
                return result; 
            }
            if (admin.Nombre.Length > 60)
            { 
                result.Success = false;
                result.Message = "El nombre no puede exceder el limite de caracteres.";
                return result; 
            }

            // --- Apellido: opcional, máximo 80 caracteres ---
            if (!string.IsNullOrWhiteSpace(admin.Apellido) && admin.Apellido.Length > 80)
            { 
                result.Success = false;
                result.Message = "El apellido no puede exceder el limite de caracteres.";
                return result; 
            }

            // --- Email: obligatorio, formato y longitud ---
            if (string.IsNullOrWhiteSpace(admin.Email))
            { 
                result.Success = false;
                result.Message = "El email es obligatorio.";
                return result; 
            }
            if (!(admin.Email.EndsWith("@gmail.com") || admin.Email.EndsWith("@hotmail.com")))
            { 
                result.Success = false;
                result.Message = "El email no tiene un formato válido.";
                return result; 
            }
            if (admin.Email.Length > 80)
            { 
                result.Success = false;
                result.Message = "El email no puede exceder el limite de caracteres.";
                return result; 
            }

            // --- Nacimiento: obligatorio, no futuro ---
            if (admin.Nacimiento == default)
            {
                result.Success = false;
                result.Message = "La fecha de nacimiento es obligatoria.";
                return result;
            }

            var hoy = DateOnly.FromDateTime(DateTime.Now);
            if (admin.Nacimiento > hoy)
            { 
                result.Success = false;
                result.Message = "La fecha de nacimiento no puede ser futura.";
                return result; 
            }

            // --- Edad: opcional, pero si existe debe ser >= 0 y <= 150 ---
            if (admin.Edad < 0)
            { 
                result.Success = false;
                result.Message = "La edad no puede ser negativa.";
                return result; 
            }
            if (admin.Edad >= 150)
            { 
                result.Success = false;
                result.Message = "La edad no puede ser mayor de 150 años.";
                return result; 
            }

            // --- AdminEstatus: obligatorio y válido ---
            if (!Enum.IsDefined(typeof(Status), admin.AdminEstatus))
            { 
                result.Success = false;
                result.Message = "El estado del admin no es válido.";
                return result; 
            }

            result.Success = true;
            result.Message = "Validación exitosa.";
            return result;
        }
    }
}
