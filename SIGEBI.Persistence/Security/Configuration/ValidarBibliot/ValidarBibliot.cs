using FluentValidation;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Persistence.Security.Configuration.ValidarBibliot
{
    public class ValidarBibliot : AbstractValidator<Bibliotecarios>
    {
        public ValidarBibliot()
        {
            // Objeto no nulo
            RuleFor(b => b).NotNull().WithMessage("El objeto Bibliotecarios no puede ser nulo.");

            // Nombre: obligatorio, max 60
            RuleFor(b => b.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(60).WithMessage("El nombre no puede exceder 60 caracteres.");

            // Apellido: opcional, max 80
            RuleFor(b => b.Apellido)
                .MaximumLength(80).WithMessage("El apellido no puede exceder 80 caracteres.")
                .When(b => !string.IsNullOrWhiteSpace(b.Apellido));

            // Género: obligatorio y dominio válido
            RuleFor(b => b.Genero)
                .NotEmpty().WithMessage("El género es obligatorio.");

            // Cédula: opcional, sólo dígitos y longitud razonable (7-15)
            RuleFor(b => b.Cedula)
                .Matches(@"^\d{7,15}$")
                .WithMessage("La cédula debe contener sólo dígitos y tener entre 7 y 15 caracteres.")
                .When(b => !string.IsNullOrWhiteSpace(b.Cedula));

            // Email: obligatorio, formato válido y max 80
            RuleFor(b => b.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .EmailAddress().WithMessage("El correo electrónico no tiene un formato válido.")
                .MaximumLength(80).WithMessage("El correo electrónico no puede exceder 80 caracteres.");

            // Nacimiento: obligatorio y no en el futuro, edad razonable
            RuleFor(b => b.Nacimiento)
                .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("La fecha de nacimiento no puede ser en el futuro.");
            // Totales: deben ser >= 0 (y opcionalmente limitar máximos razonables)
            RuleFor(b => b.TotalDevoluciones)
                .GreaterThanOrEqualTo(0).WithMessage("TotalDevoluciones debe ser mayor o igual a 0.");

            RuleFor(b => b.TotalHorasTrabajadas)
                .GreaterThanOrEqualTo(0).WithMessage("TotalHorasTrabajadas debe ser mayor o igual a 0.");

            RuleFor(b => b.TotalClientesAtendidos)
                .GreaterThanOrEqualTo(0).WithMessage("TotalClientesAtendidos debe ser mayor o igual a 0.");

            RuleFor(b => b.TotalPrestamos)
                .GreaterThanOrEqualTo(0).WithMessage("TotalPrestamos debe ser mayor o igual a 0.");

            // StatusBiblio: obligatorio y dominio válido
            RuleFor(b => b.BiblioEstatus)
                .NotEmpty().WithMessage("El estado del bibliotecario es obligatorio.");

            // IdLgOpBiblio: opcional, si se especifica debe ser >= 0
            RuleFor(b => b.IdLgOpBiblio)
                .GreaterThanOrEqualTo(0).WithMessage("IdLgOpBiblio debe ser mayor o igual a 0 si se proporciona.")
                .When(b => b.IdLgOpBiblio.HasValue);
        }

        // Helper para calcular edad exacta a partir de la fecha de nacimiento
        private static int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
