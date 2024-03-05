using Microsoft.Extensions.Hosting;
using Application;
using Infrastrutcture;
using Microsoft.Extensions.DependencyInjection;
using Application.Interfaces;


namespace Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
            {
                services.AppServices();
                services.InfraServices();
            }).Build();

            var app = host.Services.GetRequiredService<IApplication>();
            app.Run();

            Console.ReadKey();
               
            
        }
    }
}
