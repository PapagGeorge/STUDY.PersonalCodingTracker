using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace ConfigureServicesDI
{
    public class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddSingleton<IApplication, Application>();
                services.AddSingleton<IDummy, Dummy>();
            }).Build();

            var app = host.Services.GetRequiredService<IDummy>();
            var app2 = host.Services.GetRequiredService<IApplication>();
            app.DoSomething();
            app2.Run();

        }
    }
}
