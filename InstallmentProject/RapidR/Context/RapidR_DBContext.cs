using Microsoft.EntityFrameworkCore;
using RapidR.Entities;

namespace RapidR.Context
{
    public class RapidR_DBContext: DbContext
    {
        public RapidR_DBContext(DbContextOptions<RapidR_DBContext> options):base(options) { }
       public DbSet<User>  Users { get; set; }
        
    }
    
}
