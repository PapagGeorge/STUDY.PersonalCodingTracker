using LibraryApplication.Interfaces;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;


namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            var databaseConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            services.AddSingleton(databaseConfiguration);
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IBookRepository, BookRepository>();
            return services;
        }
    }
}
