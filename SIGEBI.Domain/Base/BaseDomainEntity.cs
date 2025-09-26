using System;
using System.Collections.Generic;
using System.Linq;
using SIGEBI.Domain.Enums;
namespace SIGEBI.Domain.Base
{
    public abstract class BaseDomainEntity
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? LastDateUpdate { get; set; }
        public int UserModified { get; set; }
        public int? LastUserModified { get; set; }
        public Status Estatus { get; set; } = Status.Activo;
    }
}
