using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices(this IServiceCollection services)
        {
            var dbConfiguration = (DatabaseConfigurationSection)ConfigurationManager.GetSection("DataBaseConfigurationSection");
            var connectionString = dbConfiguration.ConnectionString;

            services.AddDbContext<SalesDbContext>(options => options.UseSqlServer(connectionString));
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IOrderService, OrderService>();

            return services;
        }
    }
}
