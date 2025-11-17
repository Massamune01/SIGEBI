using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public abstract class Usuario
    {
        [Key]
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 60")]
        public string Nombre { get; set; }

        [StringLength(80, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 80")]
        public string Apellido { get; set; }

        [StringLength(15, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 15")]
        public string Cedula { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? Nacimiento { get; set; }
        public int? RolId { get; set; }
    }
}
