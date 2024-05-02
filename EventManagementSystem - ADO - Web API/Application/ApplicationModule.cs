using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICrudService, CrudService>();
            return services;
        }
    }
}
