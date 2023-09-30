using Installment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Installment.Context
{
    public class InstallmentDbContext : DbContext
    {
        public InstallmentDbContext(DbContextOptions<InstallmentDbContext> options)
        : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<InstallmentPlan> Plans { get; set; }
        public DbSet<InstallmentPayment> Payment { get; set; }

    }
}
