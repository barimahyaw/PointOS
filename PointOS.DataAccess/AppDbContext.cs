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
    }
}
