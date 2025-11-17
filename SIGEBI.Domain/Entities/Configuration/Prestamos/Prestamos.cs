using System.ComponentModel.DataAnnotations;
using System.Numerics;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration.Prestamos
{
    public class Prestamos
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DatePrest { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDevol {  get; set; }

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
