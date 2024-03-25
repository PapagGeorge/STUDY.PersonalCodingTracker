using Microsoft.Extensions.DependencyInjection;
using LibraryApplication.Interfaces;
using LibraryApplication.Services;

namespace LibraryApplication
{
    public static class ApplicationModule
    {
        public static IServiceCollection AppServices (this IServiceCollection services)
        {
            services.AddSingleton<IApplication, Application>();
            return services;
        }
    }
}
