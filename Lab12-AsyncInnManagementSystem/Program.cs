using Lab12_AsyncInnManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
      {
          // Tell the authenticaion scheme "how/where" to validate the token + secret
          options.TokenValidationParameters = Models.JwtTokenService.GetValidationParameters(builder.Configuration);
      });
            
builder.Services.AddAuthorization(options =>
{
    // Add "Name of Policy", and the Lambda returns a definition
    options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
    options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
    options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
});
            

//Add to ApplicationUser or a custom DTO class

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