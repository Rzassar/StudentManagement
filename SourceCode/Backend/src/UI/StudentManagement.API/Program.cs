using Microsoft.EntityFrameworkCore;
using StudentManagement.Core.Application;
using StudentManagement.Core.Application.Contracts.IRepository;
using StudentManagement.Infrastructure;
using StudentManagement.Infrastructure.Repository;

namespace StudentManagement.UI.API
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

            //NOTE: Add services to the container.
            builder.Services.AddControllers();

            //NOTE: Add CORS policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", builder =>
                {
                    var allowedOrigins = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")?.Split(',')
                                        ?? ["http://localhost:4200"];
                    builder.WithOrigins(allowedOrigins)
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices
            (
                builder.Configuration.GetConnectionString("DefaultConnection")
            );

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCustomExceptionHandler();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            //NOTE: Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.MapControllers();

            app.Run();
        }
    }
}