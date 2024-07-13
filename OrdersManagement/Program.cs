using BLL.Interfaces;
using BLL.Repos;
using PL.Mapping;
using DAL.OrderManagementDBContext;
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



            builder.Services.AddScoped<OrderManagementDBContext>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddScoped<IProductCategoryRepo, ProductCategoryRepo>();
            
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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}