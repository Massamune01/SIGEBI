using FluentValidation;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Persistence.Security.Configuration.ValidarAdmin
{
    public class ValidarAdmin : AbstractValidator<Admin>
    {
        public ValidarAdmin()
        {
            // Objeto no nulo
            RuleFor(a => a).NotNull().WithMessage("El objeto Admin no puede ser nulo.");

            // Nombre: obligatorio, max 60
            RuleFor(a => a.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre no puede exceder 60 caracteres.");

            // Apellido: opcional, max 80
            RuleFor(a => a.Apellido)
                .MaximumLength(80).WithMessage("El apellido no puede exceder 80 caracteres.")
                .When(a => !string.IsNullOrWhiteSpace(a.Apellido));

            // Género: obligatorio y dominio válido
            RuleFor(a => a.Genero)
                .NotEmpty().WithMessage("El género es obligatorio.");

            // Email: obligatorio, formato válido y max 80
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .MaximumLength(80).WithMessage("El email no puede exceder 80 caracteres.");

            // Nacimiento: obligatorio, no futuro, edad razonable
            RuleFor(a => a.Nacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.");
            // Edad: opcional, >=0 y <=150
            RuleFor(a => a.Edad)
                .GreaterThanOrEqualTo(0).WithMessage("La edad no puede ser negativa.");

            // StatusAdmin: obligatorio y dominio válido
            RuleFor(a => a.AdminEstatus)
                .NotEmpty().WithMessage("El estado del admin es obligatorio.");

            // RolAdmin: opcional, si se especifica debe ser > 0
            RuleFor(a => a.RolId)
                .GreaterThan(0).WithMessage("RolAdmin, si se especifica, debe ser un id válido (> 0).");

            // IdLgOpAdmin: opcional, si se especifica debe ser >= 0
            RuleFor(a => a.IdLgOpAdmin)
                .GreaterThanOrEqualTo(0).WithMessage("IdLgOpAdmin debe ser mayor o igual a 0 si se especifica.")
                .When(a => a.IdLgOpAdmin.HasValue);
        }
    }
}
