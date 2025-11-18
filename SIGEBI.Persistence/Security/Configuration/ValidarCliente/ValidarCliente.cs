using FluentValidation;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Persistence.Security.Configuration.ValidarCliente
{
    public class ValidarCliente
    {
        public OperationResult ValidateCliente(Cliente cliente)
        {
            var result = new OperationResult();

            // Validación de objeto nulo
            if (cliente == null)
            {
                result.Success = false;
                result.Message = "El objeto Cliente no puede ser nulo.";
                return result;
            }

            // Nombre
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
            {
                result.Success = false;
                result.Message = "El nombre es obligatorio.";
                return result;
            }

            if (cliente.Nombre.Length > 100)
            {
                result.Success = false;
                result.Message = "El nombre no puede exceder 100 caracteres.";
                return result;
            }

            // Apellido (opcional, solo si tiene valor)
            if (!string.IsNullOrWhiteSpace(cliente.Apellido) && cliente.Apellido.Length > 80)
            {
                result.Success = false;
                result.Message = "El apellido no puede exceder 80 caracteres.";
                return result;
            }

            // Email
            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                result.Success = false;
                result.Message = "El email es obligatorio.";
                return result;
            }

            if (!cliente.Email.Contains("@") || !cliente.Email.Contains("."))
            {
                result.Success = false;
                result.Message = "El email no tiene un formato válido.";
                return result;
            }

            if (cliente.Email.Length > 80)
            {
                result.Success = false;
                result.Message = "El email no puede exceder 80 caracteres.";
                return result;
            }

            //Cedula
            if(string.IsNullOrWhiteSpace(cliente.Cedula))
            {
                result.Success = false;
                result.Message = "La cédula es obligatoria.";
                return result;
            }

            // Nacimiento
            if (cliente.Nacimiento == null)
            {
                result.Success = false;
                result.Message = "La fecha de nacimiento es obligatoria.";
                return result;
            }

            // Totales no negativos
            if (cliente.TotalDevoluciones < 0)
            {
                result.Success = false;
                result.Message = "TotalDevoluciones debe ser mayor o igual a 0.";
                return result;
            }

            if (cliente.CapacidadPrest < 0)
            {
                result.Success = false;
                result.Message = "CapacidadPrest debe ser mayor o igual a 0.";
                return result;
            }

            if (cliente.TotalDevolRestrasadas < 0)
            {
                result.Success = false;
                result.Message = "TotalDevolRestrasadas debe ser mayor o igual a 0.";
                return result;
            }

            if (cliente.TotalPrestamos < 0)
            {
                result.Success = false;
                result.Message = "TotalPrestamos debe ser mayor o igual a 0.";
                return result;
            }

            if (cliente.PrestamosActivos < 0)
            {
                result.Success = false;
                result.Message = "PrestamosActivos debe ser mayor o igual a 0.";
                return result;
            }

            // Consistencia: PrestamosActivos <= CapacidadPrest
            if (cliente.CapacidadPrest > 0 && cliente.PrestamosActivos > cliente.CapacidadPrest)
            {
                result.Success = false;
                result.Message = "PrestamosActivos no puede ser mayor que la CapacidadPrest del cliente.";
                return result;
            }

            // StatusCliente (Enum)
            if (!Enum.IsDefined(typeof(Status), cliente.StatusCliente))
            {
                result.Success = false;
                result.Message = "El StatusCliente no es un valor válido.";
                return result;
            }

            // Si pasa todas las validaciones
            result.Success = true;
            return result;
        }
    }
}