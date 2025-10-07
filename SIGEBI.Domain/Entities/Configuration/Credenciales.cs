using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Domain.Entities.Configuration
{
    public class Credenciales
    {
        public int Id { get; set; }
        public int? ClienteId { get; set; }
        public string? Usuario { get; set; } = string.Empty;
        public string? PasswordHash { get; set; } = string.Empty;
        public Cliente? Cliente { get; set; }
    }
}
