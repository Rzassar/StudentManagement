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

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices
            (
                builder.Configuration.GetConnectionString("DefaultConnection")
            );

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //NOTE: Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}