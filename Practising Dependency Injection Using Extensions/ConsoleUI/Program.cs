using ConsoleUI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<IApplication, Application>();
                services.AddSingleton<IDummy,Dummy>();
            }).Build();
            var application = host.Services.GetRequiredService<IApplication>();
            application.Run();

        }
    }
}
