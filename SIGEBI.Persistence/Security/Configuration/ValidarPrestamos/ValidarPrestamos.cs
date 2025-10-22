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
    public class ValidarPrestamos : AbstractValidator<Prestamos>
    {
        public ValidarPrestamos()
        {
            // Validación general
            RuleFor(p => p).NotNull().WithMessage("El objeto Prestamo no puede ser nulo.");

            // DatePrest (fecha del préstamo) - obligatoria y no en el futuro
            RuleFor(p => p.DatePrest)
                .NotEmpty().WithMessage("La fecha de préstamo es obligatoria.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha de préstamo no puede estar en el futuro.");

            // DateDevol (fecha de devolución pactada) - obligatoria y posterior a la de préstamo
            RuleFor(p => p.DateDevol)
                .NotEmpty().WithMessage("La fecha de devolución es obligatoria.")
                .GreaterThan(p => p.DatePrest).WithMessage("La fecha de devolución debe ser posterior a la fecha de préstamo.");

            // DateWasDevol (fecha en que realmente devolvió el libro) - opcional, pero no antes del préstamo
            RuleFor(p => p.DateWasDevol)
                .GreaterThanOrEqualTo(p => p.DatePrest)
                .When(p => p.DateWasDevol.HasValue)
                .WithMessage("La fecha real de devolución no puede ser anterior a la fecha de préstamo.");

            // Status - obligatorio y debe estar entre los permitidos
            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("El estado es obligatorio.")
                .Must(s => s.Equals("Disponible" ))
                .WithMessage("El estado debe ser 'Disponible' o 'Inactivo'.");

            // IdLibros - obligatorio (no debe ser cero)
            RuleFor(p => p.IdLibros)
                .GreaterThan(0).WithMessage("Debe especificar un libro válido.");

            // IdCliente - obligatorio (no debe ser cero)
            RuleFor(p => p.IdCliente)
                .GreaterThan(0).WithMessage("Debe especificar un cliente válido.");

            // IdLgOpPrest - opcional, pero si se manda debe ser positivo
            RuleFor(p => p.IdLgOpPrest)
                .GreaterThan(0).When(p => p.IdLgOpPrest.HasValue)
                .WithMessage("El Id de LogOperation, si se especifica, debe ser mayor a 0.");
        }
    }
}
