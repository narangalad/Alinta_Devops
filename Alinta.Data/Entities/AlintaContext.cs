using Microsoft.EntityFrameworkCore;

namespace Alinta.Data.Entities
{
    public partial class AlintaContext : DbContext
    {
        public AlintaContext(DbContextOptions<AlintaContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
