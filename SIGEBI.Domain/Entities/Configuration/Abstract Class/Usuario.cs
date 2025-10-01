using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public abstract class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        public string? Email { get; set; }
        public DateTime? Nacimiento { get; set; }
        public int RolId { get; set; }
        public Roles Rol { get; set; } = null!;

        
    }
}
