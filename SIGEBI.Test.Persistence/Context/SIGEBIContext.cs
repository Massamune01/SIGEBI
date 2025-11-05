
using Microsoft.EntityFrameworkCore;

namespace SIGEBI.Test.Persistence.Context
{
    public class SIGEBIContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SIGEBI");
        }
    }
}
