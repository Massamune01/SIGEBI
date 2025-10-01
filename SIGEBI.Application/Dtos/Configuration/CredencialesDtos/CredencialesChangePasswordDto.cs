using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Application.Dtos.Configuration.CredencialesDtos
{
    public record CredencialesChangePasswordDto
    {
        public int ClienteId { get; set; }         
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
