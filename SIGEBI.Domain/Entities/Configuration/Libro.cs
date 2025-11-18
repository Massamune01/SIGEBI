using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public class Libro
    {
        [Key]
        public Int64 ISBN { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 100")]
        public string titulo { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 100")]
        public string autor {  get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 100")]
        public string editorial { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int anoPublicacion { get; set; }

        [StringLength(80, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 80")]
        public string categoria { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int numPaginas { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int cantidad { get; set; }
        public Status Status { get; set; } = Status.Activo;
        public int? IdLgOpLibro { get; set; }
    }
}
