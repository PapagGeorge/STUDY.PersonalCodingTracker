using Infrastrutcture.Interfaces;
using Infrastrutcture.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Infrastrutcture
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices (this IServiceCollection services)
        {
            var databaseConfiguration = (DataBaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection");

            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddSingleton(databaseConfiguration);
            return services;
        }
    }
}
