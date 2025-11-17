using System.ComponentModel.DataAnnotations;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public class Cliente : Usuario
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int TotalDevoluciones {  get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int CapacidadPrest {  get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int TotalDevolRestrasadas { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int TotalPrestamos { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int PrestamosActivos { get; set; }
        public Status StatusCliente { get; set; }
        public int? IdLgOpCliente { get; set; }
    }
}
