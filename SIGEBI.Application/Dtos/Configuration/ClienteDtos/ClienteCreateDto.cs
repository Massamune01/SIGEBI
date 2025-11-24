using System.ComponentModel.DataAnnotations;
using SIGEBI.Application.Dtos.BaseDtos.UserDtos;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.ClienteDtos
{
    public record ClienteCreateDto : UsuarioCreateDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? TotalDevoluciones { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? CapacidadPrest { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? TotalDevolRestrasadas { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? TotalPrestamos { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? PrestamosActivos { get; set; }

        public Status StatusCliente { get; set; } = Status.Activo;
        public int? IdLgOpCliente { get; set; } = 0;

    }
}
