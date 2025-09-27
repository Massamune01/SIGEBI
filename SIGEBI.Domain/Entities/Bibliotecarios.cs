using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public sealed class Bibliotecarios : Usuario
    {
        public int TotalDevoluciones { get; set; }
        public int TotalHorasTrabajadas { get; set; }
        public int totalClientesAtendidos { get; set; }
        public int TotalPrestamos { get; set; }

        public Status BiblioEstatus { get; set; }
    }
}
