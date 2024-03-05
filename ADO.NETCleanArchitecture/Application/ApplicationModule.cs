using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AppServices (this IServiceCollection services)
        {
            services.AddSingleton<IApplication, Services.Application>();
            return services;
        }
    }
}
