using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
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

            // DateWasDevol (fecha real de devolución, opcional)
            if (p.DateWasDevol.HasValue && p.DateWasDevol.Value < p.DatePrest)
            {
                result.Success = false;
                result.Message = "La fecha real de devolución no puede ser anterior a la fecha de préstamo.";
                return result;
            }

            // IdLibro
            if (p.IdLibros <= 0)
            {
                result.Success = false;
                result.Message = "Debe especificar un libro válido.";
                return result;
            }

            // IdCliente
            if (p.IdCliente <= 0)
            {
                result.Success = false;
                result.Message = "Debe especificar un cliente válido.";
                return result;
            }

            // Si todo es válido
            result.Success = true;
            result.Message = "Validación exitosa.";
            return result;
        }
    }
}
