using BLL.Interfaces;
using BLL.Repos;
using PL.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DAL.OrderManagementDBContext
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Connection-String
            builder.Services.AddDbContext<OrderManagementDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString
                ("Server=.\\SQLEXPRESS;Database=OrderManagementDBContext;Trusted_Connection=True; Encrypt=false;")));

            // CONFIG
            builder.Services.AddAutoMapper(map => map.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<IProductCategoryRepo, ProductCategoryRepo>();
            builder.Services.AddScoped<UploadFileRepo>();
            builder.Services.AddScoped<IUploadFileRepo, UploadFileRepo>();
            builder.Services.AddScoped<IProductPriceHistoryRepo, ProductPriceHistoryRepo>();
            builder.Services.AddScoped<ICartRepo, CartRepo>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddDistributedMemoryCache(); // Required for session state

            // Add session services with options
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Make the cookie accessible only via HTTP
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Self-Config
            app.UseSession(); // Ensure session middleware is called & after app.UseRouting but B4 app.UseAuthorization


            app.UseAuthorization();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=Marketplace}/{id?}");

            app.Run();
        }
    }
}