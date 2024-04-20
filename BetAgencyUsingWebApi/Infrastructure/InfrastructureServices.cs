using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            services.AddDbContext<BetDbContext>(options => options.UseSqlServer(dbConfiguration.ConnectionString));
            return services;

        }
    }
}
