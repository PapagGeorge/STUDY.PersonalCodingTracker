using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ConsoleUI.Services;
using ClassLibrary1;
using ClassLibrary_ModuleExample;




namespace ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddTransient<IApplication2, Application2> ();
                services.AddTransient<IApplication, Application>();
                services.AddTransient<IApplicationFromAnotherProject, ApplicationFromAnotherProject>();
                services.AddServices();
                

            }).Build();


            
            var app1 = host.Services.GetRequiredService<IApplication>();
            app1.Run();
            
        }
    }
}
