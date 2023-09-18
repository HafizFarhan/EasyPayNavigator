
using Installment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Installment
{
    public class InstallmentDbContext : DbContext
    {
        public InstallmentDbContext(DbContextOptions<InstallmentDbContext> options)
        : base(options)
        {
        }

        public DbSet<Products> Products { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<InstallmentPayment> InstallmentPayment { get; set; }
        public DbSet<InstallmentPlan> InstallmentPlan { get; set; }

    }
}
