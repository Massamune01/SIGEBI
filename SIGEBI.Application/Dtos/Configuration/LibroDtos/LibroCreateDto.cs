using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LibroDtos
{
    public class LibroCreateDto
    {
        [Key]
        public Int64 ISBN { get; set; }
        public string titulo { get; set; } = null!;
        public string autor { get; set; } = null!;
        public string editorial { get; set; }
        public int anoPublicacion { get; set; }
        public string categoria { get; set; }
        public int numPaginas { get; set; }
        public int cantidad { get; set; }
        public Status Status { get; set; } = Status.Activo;

    }
}
