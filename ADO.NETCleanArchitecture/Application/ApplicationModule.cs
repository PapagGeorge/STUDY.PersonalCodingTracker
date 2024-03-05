using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
