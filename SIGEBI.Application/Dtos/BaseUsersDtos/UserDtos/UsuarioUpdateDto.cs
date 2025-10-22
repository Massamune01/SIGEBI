using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.BaseDtos.UserDtos
{
    public record UsuarioUpdateDto
    {
        [Required]
        public int Id { get; set; }

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        public string? Email { get; set; }
        public DateOnly? Nacimiento { get; set; }
        public int RolId { get; set; }
        public virtual int? IdLgOp { get; set; }
    }
}
