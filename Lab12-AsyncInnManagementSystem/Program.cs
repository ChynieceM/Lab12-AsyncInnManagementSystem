using Lab12_AsyncInnManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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


            builder.Services.AddSwaggerGen(options =>
            {
                // Make sure get the "using Statement"
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Async Inn",
                    Version = "v1",
                });
            });

            builder.Services.AddDbContext<AsyncInnContext>(options => 
            options.UseSqlServer(
                builder.Configuration
                .GetConnectionString("DefaultConnection")));
            var app = builder.Build();

            //swagger documentName = version parameter from builder
            app.UseSwagger(options => {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Async Inn");
                options.RoutePrefix = "docs";
            });
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