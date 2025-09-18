using Microsoft.EntityFrameworkCore;
using TableToSTO.Models.Entities;
using TableToSTO.Services.S0305;
using TableToSTO.Services.S0305.SI0305;

namespace TableToSTO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            // DbContext
            builder.Services.AddDbContext<TableToSTOContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TableToSTOContext"))
            );

            // Services
            builder.Services.AddScoped<SI0305PhieuNhapService, S0305PhieuNhapService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
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
