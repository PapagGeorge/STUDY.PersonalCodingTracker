using ClassLibrary_ModuleExample.Services;
using Microsoft.Extensions.DependencyInjection;


namespace ClassLibrary_ModuleExample
{
    public static class Services_Module
    {
        public static IServiceCollection GetServices(this IServiceCollection services)
        {
            services.AddTransient<IBottle, Bottle>();
            services.AddTransient<ITable, Table>();
            return services;
        }


    }
}
