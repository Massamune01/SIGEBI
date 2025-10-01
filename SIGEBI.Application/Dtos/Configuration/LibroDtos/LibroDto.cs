using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LibroDtos
{
    public class LibroDto
    {
        public int ISBN { get; set; }
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string Editorial { get; set; } = null!;
        public int AnioPublicacion { get; set; }
        public string Categoria { get; set; } = null!;
        public int NumPaginas { get; set; }
        public int Cantidad { get; set; }
        public bool Disponible { get; set; }
        public int? IdLgOpLibro { get; set; }
        public Status Status { get; set; } = Status.Activo;
    }
}
