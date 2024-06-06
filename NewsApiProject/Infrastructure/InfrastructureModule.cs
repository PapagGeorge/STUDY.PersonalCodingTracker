using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;

namespace Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MsSql");

            services.AddDbContext<NewsDbContext>(options => options.UseSqlServer(connectionString));
            return services;
            }
    }
}
