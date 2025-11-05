using SIGEBI.Domain.Base;
using SIGEBI.Domain.Entities.Configuration;

namespace SIGEBI.Persistence.Security.Configuration.ValidarLibro
{
    public sealed class ValidarLibro
    {
        public OperationResult ValidateLibro(Libro l)
        {
            var result = new OperationResult();

            if (l == null)
            {
                result.Success = false;
                result.Message = "El objeto Libro no puede ser nulo.";
                return result;
            }

            // ISBN
            if (l.ISBN.ToString().Length != 10 && l.ISBN.ToString().Length != 13)
            {
                result.Success = false;
                result.Message = "El ISBN debe tener 10 o 13 dígitos.";
                return result;
            }

            // Título
            if (string.IsNullOrWhiteSpace(l.titulo))
            {
                result.Success = false;
                result.Message = "El campo título no puede estar vacío.";
                return result;
            }

            if (l.titulo.Length > 100)
            {
                result.Success = false;
                result.Message = "El campo título no puede exceder los 100 caracteres.";
                return result;
            }

            // Autor
            if (string.IsNullOrWhiteSpace(l.autor))
            {
                result.Success = false;
                result.Message = "El campo autor no puede estar vacío.";
                return result;
            }

            if (l.autor.Length > 100)
            {
                result.Success = false;
                result.Message = "El campo autor no puede exceder los 100 caracteres.";
                return result;
            }

            // Editorial
            if (string.IsNullOrWhiteSpace(l.editorial))
            {
                result.Success = false;
                result.Message = "El campo editorial no puede estar vacío.";
                return result;
            }

            if (l.editorial.Length > 100)
            {
                result.Success = false;
                result.Message = "El campo editorial no puede exceder los 100 caracteres.";
                return result;
            }

            // Año de publicación
            if (l.anoPublicacion <= 0)
            {
                result.Success = false;
                result.Message = "El campo año de publicación debe ser un número positivo.";
                return result;
            }

            // Categoría
            if (string.IsNullOrWhiteSpace(l.categoria))
            {
                result.Success = false;
                result.Message = "El campo categoría no puede estar vacío.";
                return result;
            }

            if (l.categoria.Length > 80)
            {
                result.Success = false;
                result.Message = "El campo categoría no puede exceder los 80 caracteres.";
                return result;
            }

            // Número de páginas
            if (l.numPaginas < 0)
            {
                result.Success = false;
                result.Message = "El campo número de páginas debe ser un número positivo.";
                return result;
            }

            // Cantidad
            if (l.cantidad < 0)
            {
                result.Success = false;
                result.Message = "El campo cantidad debe ser un número positivo.";
                return result;
            }

            // Validación completada correctamente
            result.Success = true;
            result.Message = "Validación completada correctamente.";
            result.Data = l;
            return result;
        }
    }
}