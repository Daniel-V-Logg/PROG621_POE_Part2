using Microsoft.EntityFrameworkCore;

namespace Contract_Monthly_ClaimSystem__CMCS_.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; } // Add your DbSet for Claims

        // Override this method if you want to configure the model or relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        
        }
    }
}