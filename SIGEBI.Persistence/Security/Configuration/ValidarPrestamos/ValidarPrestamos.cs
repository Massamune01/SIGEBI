using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration.Prestamos;

namespace SIGEBI.Persistence.Security.Configuration.ValidarPrestamos
{
    public class ValidarPrestamos
    {
        public OperationResult ValidatePrestamo(Prestamos p)
        {
            var result = new OperationResult();

            // Validar objeto nulo
            if (p == null)
            {
                result.Success = false;
                result.Message = "El objeto Prestamo no puede ser nulo.";
                return result;
            }

            // DatePrest (fecha del préstamo)
            if (p.DatePrest == default)
            {
                result.Success = false;
                result.Message = "La fecha de préstamo es obligatoria.";
                return result;
            }

            if (p.DatePrest > DateTime.Now)
            {
                result.Success = false;
                result.Message = "La fecha de préstamo no puede estar en el futuro.";
                return result;
            }

            // DateDevol (fecha de devolución pactada)
            if (p.DateDevol == default)
            {
                result.Success = false;
                result.Message = "La fecha de devolución es obligatoria.";
                return result;
            }

            if (p.DateDevol <= p.DatePrest)
            {
                result.Success = false;
                result.Message = "La fecha de devolución debe ser posterior a la fecha de préstamo.";
                return result;
            }

            // Si todo es válido
            result.Success = true;
            result.Message = "Validación exitosa.";
            return result;
        }
    }
}
