using FluentValidation;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Persistence.Security.Configuration.ValidarRol
{
    public class ValidarRol : AbstractValidator<Roles>
    {
        public ValidarRol()
        {
            // Objeto no nulo
            RuleFor(r => r).NotNull().WithMessage("El objeto Rol no puede ser nulo.");

            // Rol: obligatorio, trim, max 30 caracteres
            RuleFor(r => r.Rol)
                .NotEmpty().WithMessage("El nombre del rol es obligatorio.")
                .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage("El nombre del rol no puede ser sólo espacios.")
                .MaximumLength(30).WithMessage("El nombre del rol no puede exceder 30 caracteres.");

            // StatusRol: obligatorio y dentro del conjunto permitido
            RuleFor(r => r.RolEstatus)
                .NotEmpty().WithMessage("El estado del rol es obligatorio.");
            // IdLgOpRol: opcional, si se proporciona debe ser >= 0
            RuleFor(r => r.IdLgOpRol)
                .GreaterThanOrEqualTo(0)
                .When(r => r.IdLgOpRol.HasValue)
                .WithMessage("IdLgOpRol debe ser mayor o igual a 0 si se proporciona.");

            // Id: si se valida en actualizaciones, debe ser > 0
            RuleFor(r => r.Id)
                .GreaterThan(0)
                .When(r => r.Id != 0)
                .WithMessage("Id del rol inválido.");

            // Ejemplo de regla condicional: nombre mínimo cuando es rol sensible (ej: 'Admin')
            RuleFor(r => r.Rol)
                .MinimumLength(3)
                .When(r => string.Equals(r.Rol, "Admin", StringComparison.OrdinalIgnoreCase) == false)
                .WithMessage("El nombre del rol debe tener al menos 3 caracteres.");

        }
    }
}
