using DAL.Entities;
using Microsoft.EntityFrameworkCore;



namespace DAL.OrderManagementDBContext 
{
    public class OrderManagementDBContext : DbContext
    {
        public OrderManagementDBContext(DbContextOptions<OrderManagementDBContext> options) : base(options){ }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}
