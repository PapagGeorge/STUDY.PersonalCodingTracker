using Microsoft.Extensions.DependencyInjection;
using ClassLibrary_ModuleExample.Services;

namespace ClassLibrary_ModuleExample
{
    public static class ServicesModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IBottle,Bottle>();
            services.AddTransient<ITable, Table>();
            return services;
        }

    }
}
