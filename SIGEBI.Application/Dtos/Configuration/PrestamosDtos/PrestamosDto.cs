using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.PrestamosDtos
{
    public class PrestamoDto
    {
        [Key]
        public int Id { get; set; }
        public DateTime DatePrest { get; set; }
        public DateTime DateDevol { get; set; }
        public DateTime? DateWasDevol { get; set; }
        public Status Status { get; set; } = Status.Activo;
        public Int64 IdLibros { get; set; }
        public int IdCliente { get; set; }
        public int? IdLgOpPrest { get; set; }
    }
}
