using DI_Practise.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ClassLibrary_ModuleExample;
using ClassLibrary1;


namespace DI_Practise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            { services.AddTransient<IApplication2, Application2>();
              services.AddTransient<IApplication, Application>();
              services.AddTransient<IClassFromAnotherProject, ClassFromAnotherProject>();
              services.GetServices();
            
            }).Build();

            var app = host.Services.GetRequiredService<IApplication>();
            app.Run();
        }
    }
}
