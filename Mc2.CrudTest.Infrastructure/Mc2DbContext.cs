using Mc2.CrudTest.Domain.Customers;
using Mc2.CrudTest.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Infrastructure
{
    public class Mc2DbContext : DbContext
    {
        public Mc2DbContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}
