using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public sealed class Admin : Usuario 
    {
        public Status AdminEstatus { get; set; } = Status.Activo;
    }
}
