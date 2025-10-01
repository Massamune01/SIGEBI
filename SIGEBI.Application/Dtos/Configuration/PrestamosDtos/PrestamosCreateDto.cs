using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGEBI.Application.Dtos.Configuration.PrestamosDtos
{
    public record PrestamoCreateDto
    {
        public DateTime DatePrest { get; set; }   
        public DateTime DateDevol { get; set; }             
        
        public int IdLibros { get; set; }                
        public int IdCliente { get; set; }
        public int? IdLgOpLibro { get; set; }
    }
}
