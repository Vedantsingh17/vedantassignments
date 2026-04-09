using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ProductApi.Services;
using ProductAPI.Data;
using ProductAPI.Servise;

namespace ProductAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("AzureSqlConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductService, ProductService>();

            // Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // ✅ Configure Swagger for both Dev & Production
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}