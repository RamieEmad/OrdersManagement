using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



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
                .HasOne(p => p.ProductCategory)

                .WithMany(pc => pc.Products)   

                .HasForeignKey(p => p.ProductCategoryId);
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<OrderManagementDBContext>
    {
        public OrderManagementDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderManagementDBContext>();
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=OrderManagementDBContext;Trusted_Connection=True; Encrypt=false;");

            return new OrderManagementDBContext(optionsBuilder.Options);
        }
    }
}
