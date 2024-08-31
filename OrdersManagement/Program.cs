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
           

            builder.Services.AddDbContext<OrderManagementDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString
                ("Server=.\\SQLEXPRESS;Database=OrderManagementDBContext;Trusted_Connection=True; Encrypt=false;")));

            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<IProductCategoryRepo, ProductCategoryRepo>();
            builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            builder.Services.AddAutoMapper(map => map.AddProfile(new MappingProfile()));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=List}/{id?}");

            app.Run();
            
        }
    }
}