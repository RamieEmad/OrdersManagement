using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;



namespace DAL.OrderManagementDBContext
{
    public class OrderManagementDBContext : DbContext
    {
        public OrderManagementDBContext(DbContextOptions<OrderManagementDBContext> options) : base(options) 
        { 

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductPriceHistory> ProductPriceHistories { get; set; }
        public DbSet <UploadFile> UploadFiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Server=.\SQLEXPRESS;Database=OrderManagementDBContext;Trusted_Connection=True; Encrypt=false;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            //one-to-many { Product => ProductCategory }
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductCategory)
                .WithMany(pc => pc.Products)
                .HasForeignKey(p => p.ProductCategoryId);

            ////one-to-many { Product => ProductPriceHistories }
            //modelBuilder.Entity<Product>()
            //    .HasMany(p => p.ProductPriceHistories)
            //    .WithOne(pph => pph.Product)
            //    .HasForeignKey(pph => pph.ProductId);
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
