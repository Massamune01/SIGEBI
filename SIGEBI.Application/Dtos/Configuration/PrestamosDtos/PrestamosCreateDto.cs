using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Application.Dtos.Configuration.PrestamosDtos
{
    public record PrestamoCreateDto
    {
        [DataType(DataType.Date)]
        public DateTime DatePrest { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDevol { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public Int64 IdLibros { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int IdCliente { get; set; }
        public int? IdLgOpLibro { get; set; }
    }
}
