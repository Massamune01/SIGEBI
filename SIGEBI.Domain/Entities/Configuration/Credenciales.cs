using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Domain.Entities.Configuration
{
    public class Credenciales
    {
        [Key]
        public int Id { get; set; }
        public int? ClienteId { get; set; }

        [StringLength(80, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 80")]
        public string? Usuario { get; set; } = string.Empty;

        [PasswordPropertyText]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres")]
        public string? PasswordHash { get; set; } = string.Empty;
        public Cliente? Cliente { get; set; }
    }
}
