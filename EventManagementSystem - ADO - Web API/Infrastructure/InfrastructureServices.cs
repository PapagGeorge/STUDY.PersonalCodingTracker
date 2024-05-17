using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using System.Configuration;


namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            var databaseConfiguration = (DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection");
            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<DatabaseConfiguration>();

            return services;    
        }
    }
}
