using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AppService(this IServiceCollection services)
        {
            services.AddScoped<IApplication, Application>();

            return services;

        }
    }
}
