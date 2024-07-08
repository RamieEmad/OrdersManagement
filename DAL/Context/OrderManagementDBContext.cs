using DAL.Entities;
using System.Data.Entity;




namespace DAL.OrderManagementDBContext 
{
    public class OrderManagementDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}
