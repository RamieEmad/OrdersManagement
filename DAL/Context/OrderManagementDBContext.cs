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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory) // Each Product has one ProductCategory
                .WithMany(pc => pc.Products)   // Each ProductCategory can have many Products
                .HasForeignKey(p => p.ProductCategoryId); // Foreign key configuration
        }


    }
}
