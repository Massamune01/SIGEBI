using System.ComponentModel.DataAnnotations;
using SIGEBI.Application.Dtos.BaseDtos.UserDtos;

namespace SIGEBI.Application.Dtos.Configuration.BibliotecariosDtos
{
    public record BibliotecarioCreateDto : UsuarioCreateDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? TotalDevoluciones { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? TotalHorasTrabajadas { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? TotalClientesAtendidos { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar un número válido.")]
        public int? TotalPrestamos { get; set; }

    }
}
