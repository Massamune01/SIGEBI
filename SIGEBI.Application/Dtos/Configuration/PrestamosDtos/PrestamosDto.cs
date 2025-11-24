using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.PrestamosDtos
{
    public class PrestamoDto
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePrest { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDevol { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateWasDevol { get; set; }
        public Status Status { get; set; } = Status.Activo;

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public Int64 IdLibros { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int IdCliente { get; set; }
        public int? IdLgOpPrest { get; set; }
    }
}
