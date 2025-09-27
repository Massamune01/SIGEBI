using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public sealed class Cliente : Usuario
    {
        public int? totalDevoluciones {  get; set; }
        public int? totalDevolRetrasadas { get; set; } 
        public int? totalPrestamos { get; set; } 
        public int? CapacidadPrest {  get; set; }
        public int? PrestamosActivos { get; set; }
        public Status ClienteEstatus { get; set; } = Status.Activo;
    }
}
