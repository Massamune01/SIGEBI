using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LibroDtos
{
    public class LibroCreateDto
    {
        [Required]
        public int ISBN { get; set; }

        [Required]
        public string titulo { get; set; } = null!;

        [Required]
        public string autor { get; set; } = null!;

        public string editorial { get; set; }
        public int anioPublicacion { get; set; }
        public string categoria { get; set; }
        public int numPaginas { get; set; }
        public int cantidad { get; set; }
        public int IdLgOpLibro { get; set; }
        public Status Status { get; set; } = Status.Activo;

    }
}
