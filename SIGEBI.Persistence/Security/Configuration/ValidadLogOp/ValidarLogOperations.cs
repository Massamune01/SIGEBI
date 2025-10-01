using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using SIGEBI.Domain.Entities.Configuration;
using SIGEBI.Persistence.Base;

namespace SIGEBI.Persistence.Security.Configuration.ValidadLogOp
{
    using FluentValidation;
    using SIGEBI.Domain.Base;

    public sealed class ValidarLogOperations : AbstractValidator<LogOperations>
    {
        public ValidarLogOperations()
        {
            // Validar objeto no nulo
            RuleFor(l => l).NotNull().WithMessage("El objeto LogOperation no puede ser nulo.");

            // Entity obligatorio y máximo 90 caracteres
            RuleFor(l => l.Entity)
                .NotEmpty().WithMessage("El campo Entity no puede estar vacío.")
                .MaximumLength(90).WithMessage("El campo Entity no puede exceder los 90 caracteres.");

            // Fecha obligatoria y no en el futuro
            RuleFor(l => l.Fecha)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede estar en el futuro.");

            // TypeOperation obligatorio y máximo 100 caracteres
            RuleFor(l => l.TypeOperation)
                .NotEmpty().WithMessage("El campo TypeOperation no puede estar vacío.")
                .MaximumLength(100).WithMessage("El campo TypeOperation no puede exceder los 100 caracteres.");

            // Descripción opcional pero máximo 100 caracteres
            RuleFor(l => l.Descripcion)
                .MaximumLength(100).WithMessage("La descripción no puede exceder los 100 caracteres.");

            // StatusOp obligatorio y validación de dominio
            RuleFor(l => l.StatusOp)
                .NotEmpty().WithMessage("El campo StatusOp no puede estar vacío.")
                .Must(s => s == "Activo" || s == "Inactivo" || s == "Pendiente")
                .WithMessage("El campo StatusOp solo puede ser 'Activo', 'Inactivo' o 'Pendiente'.");

            // LastUpdateBy y UpdateBy opcionales, pero deben ser fechas válidas si se proporcionan
            RuleFor(l => l.LastUpdateBy)
                .LessThanOrEqualTo(DateTime.Now)
                .When(l => l.LastUpdateBy.HasValue)
                .WithMessage("LastUpdateBy no puede estar en el futuro.");

            RuleFor(l => l.UpdateBy)
                .LessThanOrEqualTo(DateTime.Now)
                .When(l => l.UpdateBy.HasValue)
                .WithMessage("UpdateBy no puede estar en el futuro.");
        }
    }

}
