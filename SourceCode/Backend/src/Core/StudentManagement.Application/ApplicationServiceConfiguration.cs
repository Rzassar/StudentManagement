using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace StudentManagement.Core.Application
{
    public static class ApplicationServiceConfiguration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}