using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.LibroDtos
{
    public class LibroDto
    {
        [Key]
        public Int64 ISBN { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public int anoPublicacion { get; set; }
        public string categoria { get; set; }
        public int numPaginas { get; set; }
        public int cantidad { get; set; }
        public Status Status { get; set; }
        public int? IdLgOpLibro { get; set; }
    }
}
