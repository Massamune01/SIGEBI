using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public abstract class Usuario : BaseDomainEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string? Genero { get; set; }
        public string? Email { get; set; }
        public DateTime? Birthday { get; set; }
        public Status UserEstatus { get; set; } = Status.Activo;
        public int RolId { get; set; }
        public Roles Rol { get; set; } = null!;

        
    }
}
