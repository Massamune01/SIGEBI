using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Application.Dtos.Configuration.CredencialesDtos
{
    public record CredencialesUpdateDto
    {
        public int Id { get; set; }                 
        public string Usuario { get; set; } = string.Empty;
    }
}
