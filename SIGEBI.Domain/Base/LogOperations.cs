using System.Reflection.Metadata;

namespace SIGEBI.Domain.Base
{
    public class LogOperations : BaseDomainEntity
    {
        public int ID { get; set; }
        public string EntityName { get; set; }
        public int EntityId { get; set; }
        public string TypeOperation { get; set; }
        public string Descripcion { get; set; }
    }
}
