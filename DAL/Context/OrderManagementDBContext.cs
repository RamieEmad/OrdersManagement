using DAL.Entities;
using Microsoft.EntityFrameworkCore;



namespace DAL.OrderManagementDBContext 
{
    public class OrderManagementDBContext : DbContext
    {
        public OrderManagementDBContext(DbContextOptions<OrderManagementDBContext> options) : base(options){ }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Server=.\SQLEXPRESS;Database=OrderManagementDBContext;Trusted_Connection=True; Encrypt=false;");

            base.OnConfiguring(optionsBuilder);
        }

    }
}
