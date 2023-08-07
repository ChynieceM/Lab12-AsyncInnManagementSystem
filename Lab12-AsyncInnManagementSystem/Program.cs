using Lab12_AsyncInnManagementSystem.Data;
using Lab12_AsyncInnManagementSystem.Models;
using Lab12_AsyncInnManagementSystem.Models.Interfaces;
using Lab12_AsyncInnManagementSystem.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Lab12_AsyncInnManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AsyncInnContext>(options => 
            options.UseSqlServer(
                builder.Configuration
                .GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient<IHotel, HotelService>();
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            //middleware

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            //https://localhost:44391/Hotel/CheckIn/1
            app.Run();
        }
    }
}