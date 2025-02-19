using Microsoft.EntityFrameworkCore;

namespace EcommerceWebApp.Models
{
    public class EcommContext : DbContext
    {
        public EcommContext(DbContextOptions opt) : base(opt)
        {
            
        }

        public DbSet<Admin> tbl_admin { get; set; }
        public DbSet<Category> tbl_category { get; set; }
        public DbSet<Product> tbl_product { get; set; }
    }
}
