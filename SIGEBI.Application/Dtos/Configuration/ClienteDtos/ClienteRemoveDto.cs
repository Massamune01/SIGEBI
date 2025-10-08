using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Configuration.ClienteDtos
{
    public record ClienteRemoveDto
    {
        public int Id { get; set; }                // Identificador del cliente
        public Status StatusCliente { get; set; } = Status.Inactivo;  // Estado que tomará (soft delete)
        public int? IdLgOpCliente { get; set; }    // Opcional: log de operación
    }
}
