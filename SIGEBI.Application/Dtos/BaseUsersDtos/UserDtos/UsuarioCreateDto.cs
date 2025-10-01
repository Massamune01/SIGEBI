using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.BaseDtos.UserDtos
{
    public record UsuarioCreateDto
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string Cedula { get; set; } = null!;

        public int Edad { get; set; }

        public Genero Genero { get; set; }     

        [EmailAddress]
        public string? Email { get; set; }

        public DateTime? Nacimiento { get; set; }

        [Required]
        public int RolId { get; set; }

        public int? IdLgOpLibro { get; set; }
    }

}
