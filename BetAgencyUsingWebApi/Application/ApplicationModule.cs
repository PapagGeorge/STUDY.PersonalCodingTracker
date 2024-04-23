using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IApplication,  Application>();
            services.AddScoped<ICalculateOdds, CalculateOdds>();
            services.AddScoped < ICalculatePotentialPayout, CalculatePotentialPayout>();
            services.AddScoped<ITotalStake, TotalStake>();

            return services;

        }
    }
}
