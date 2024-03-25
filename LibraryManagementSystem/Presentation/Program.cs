using LibraryApplication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Infrastructure;
using LibraryApplication.Interfaces;

namespace Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AppServices();
                    services.InfraServices();
                }).Build();

            var app = host.Services.GetRequiredService<IApplication>();
            var appScreen = new AppScreen(app);

            appScreen.RunMenuChoice();

            
        }
    }
}
