using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<DatabaseConfiguration>();

            return services;    
        }
    }
}
