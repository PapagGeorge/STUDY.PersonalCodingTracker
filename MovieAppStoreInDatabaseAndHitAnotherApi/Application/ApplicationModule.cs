using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMovieService, MovieService>();
            services.AddHttpClient();

            services.AddHttpClient<IOMDbAccess, OMDbAccess>(client =>
            {
                client.BaseAddress = new Uri("http://www.omdbapi.com/");
            });

            services.AddMemoryCache();
            return services;
        }
    }
}
