using FluentValidation;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Persistence.Security.Configuration.ValidarCliente
{
    public class ValidarCliente : AbstractValidator<Cliente>
    {
        public ValidarCliente()
        {
            // OBJETO
            RuleFor(c => c).NotNull().WithMessage("El objeto Cliente no puede ser nulo.");

            // NOMBRE (heredado de Usuario)
            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");

            // APELLIDO (heredado de Usuario) - opcional
            RuleFor(c => c.Apellido)
                .MaximumLength(80).WithMessage("El apellido no puede exceder 80 caracteres.")
                .When(c => !string.IsNullOrWhiteSpace(c.Apellido));

            // GENERO (heredado de Usuario) - puede ser string o enum en tu modelo
            RuleFor(c => c.Genero)
                .NotEmpty().WithMessage("El género es obligatorio.");

            // EMAIL (heredado de Usuario)
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("El email no tiene un formato válido.")
                .MaximumLength(80).WithMessage("El email no puede exceder 80 caracteres.");

            // NACIMIENTO (heredado de Usuario)
            RuleFor(c => c.Nacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.");

            // TOTALS: no negativos
            RuleFor(c => c.TotalDevoluciones)
                .GreaterThanOrEqualTo(0).WithMessage("TotalDevoluciones debe ser mayor o igual a 0.");

            RuleFor(c => c.CapacidadPrest)
                .GreaterThanOrEqualTo(0).WithMessage("CapacidadPrest debe ser mayor o igual a 0.");

            RuleFor(c => c.TotalDevolRestrasadas) // nota: usa el nombre según tu clase (has typo en tu modelo)
                .GreaterThanOrEqualTo(0).WithMessage("TotalDevolRestrasadas debe ser mayor o igual a 0.");

            RuleFor(c => c.TotalPrestamos)
                .GreaterThanOrEqualTo(0).WithMessage("TotalPrestamos debe ser mayor o igual a 0.");

            RuleFor(c => c.PrestamosActivos)
                .GreaterThanOrEqualTo(0).WithMessage("PrestamosActivos debe ser mayor o igual a 0.");

            // Consistencia: PrestamosActivos no puede exceder la capacidad
            RuleFor(c => c)
                .Must(c => c.CapacidadPrest == 0 || c.PrestamosActivos <= c.CapacidadPrest)
                .WithMessage("PrestamosActivos no puede ser mayor que la CapacidadPrest del cliente.");

            // StatusCliente (es enum Status en tu clase): debe ser un valor definido
            RuleFor(c => c.StatusCliente)
                .IsInEnum().WithMessage("El StatusCliente no es un valor válido.");

        }
    }
}
