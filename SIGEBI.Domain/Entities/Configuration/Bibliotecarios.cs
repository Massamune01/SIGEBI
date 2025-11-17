using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public sealed class Bibliotecarios : Usuario
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int TotalDevoluciones { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int TotalHorasTrabajadas { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int TotalClientesAtendidos { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int TotalPrestamos { get; set; }
        public Status BiblioEstatus { get; set; }
        public int? IdLgOpBiblio { get; set; }
    }
}
