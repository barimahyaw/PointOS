using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PointOS.DataAccess.Entities;

namespace PointOS.DataAccess
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPricing> ProductPricing { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
    }
}
