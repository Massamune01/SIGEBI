using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public class Cliente : Usuario
    {
        public int TotalDevoluciones {  get; set; }
        public int CapacidadPrest {  get; set; }
        public int TotalDevolRestrasadas { get; set; }
        public int TotalPrestamos { get; set; }
        public int PrestamosActivos { get; set; }
        public Status StatusCliente { get; set; }
        public int? IdLgOpCliente { get; set; }
        public Credenciales Credenciales { get; set; }
    }
}
