using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;
using System.Configuration;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection InfraServices(IServiceCollection services)
        {
            services.AddSingleton<IAnalyticsRepository, AnalyticsRepository>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();
            services.AddSingleton<ITripRepository, TripRepository>();

            return services;
        }
    }
}
