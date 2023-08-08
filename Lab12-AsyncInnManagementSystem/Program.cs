using Lab12_AsyncInnManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab12_AsyncInnManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews().AddJsonOptions(o => {
                o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddDbContext<AsyncInnContext>(options => 
            options.UseSqlServer(
                builder.Configuration
                .GetConnectionString("DefaultConnection")));
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