using System.Runtime.CompilerServices;
using FluentValidation;
using FluentValidation.Results;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Persistence.Security.Configuration.ValidarLibro
{
    public sealed class ValidarLibro : AbstractValidator<Libro>
    {
        public ValidarLibro()
        {

            //ISBN Nullable y positivo
            RuleFor(l => l.ISBN)
                .NotEmpty().WithMessage("El campo ISBN no puede estar vacío.")
                .GreaterThan(0).WithMessage("El campo ISBN debe ser un número positivo.");
            //ISBN unico
            RuleFor(l => l.ISBN.ToString()).Must(i => i.ToString().Length == 10 || i.ToString().Length == 13)
                .WithMessage("El ISBN debe tener 10 o 13 dígitos.");
            //Esto se valida en el servicio antes de actualizar el libro
            RuleFor(l => l.ISBN.ToString()).NotEqual("0000000000").WithMessage("El ISBN no puede ser '0000000000'.");
            //Titulo no nulo y maximo 100 caracteres
            RuleFor(l => l.titulo)
                .NotEmpty().WithMessage("El campo título no puede estar vacío.")
                .MaximumLength(100).WithMessage("El campo título no puede exceder los 100 caracteres.");
            //Autor no nulo y maximo 100 caracteres
            RuleFor(l => l.autor)
                .NotEmpty().WithMessage("El campo autor no puede estar vacío.")
                .MaximumLength(100).WithMessage("El campo autor no puede exceder los 100 caracteres.");
            //Editorial no nulo y maximo 100 caracteres
            RuleFor(l => l.editorial)
                .NotEmpty().WithMessage("El campo editorial no puede estar vacío.")
                .MaximumLength(100).WithMessage("El campo editorial no puede exceder los 100 caracteres.");
            //Año de publicacion no nulo y positivo
            RuleFor(l => l.anoPublicacion)
                .NotEmpty().WithMessage("El campo año de publicación no puede estar vacío.")
                .GreaterThan(0).WithMessage("El campo año de publicación debe ser un número positivo.");
            //Categoria no nulo y maximo 80 caracteres
            RuleFor(l => l.categoria)
                .NotEmpty().WithMessage("El campo categoría no puede estar vacío.")
                .MaximumLength(80).WithMessage("El campo categoría no puede exceder los 80 caracteres.");
            //Numero de paginas no nulo y positivo
            RuleFor(l => l.numPaginas)
                .NotEmpty().WithMessage("El campo número de páginas no puede estar vacío.")
                .GreaterThan(0).WithMessage("El campo número de páginas debe ser un número positivo.");
            //Cantidad no nulo y positivo
            RuleFor(l => l.cantidad)
                .NotEmpty().WithMessage("El campo cantidad no puede estar vacío.");
            //Status no nulo
            RuleFor(l => l.Status)
                .NotNull().WithMessage("El campo estado no puede estar vacío.");
            // IdLgOpLibro opcional, pero si se proporciona debe ser >= 0
            RuleFor(l => l.IdLgOpLibro)
                .GreaterThanOrEqualTo(0)
                .When(l => l.IdLgOpLibro.HasValue)
                .WithMessage("IdLgOpLibro debe ser un número positivo si se proporciona.");

        }
    }
}
