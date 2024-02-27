using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ClassLibrary_ModuleExample.Services;
using ClassLibrary1;

namespace ConsoleUI.Services
{
    public class Application : IApplication, IApplication2
    {

        private readonly IApplication2 _application2;
        private readonly ILogger _logger;
        private readonly IBottle _bottle;
        private readonly ITable _table;
        private readonly IApplicationFromAnotherProject _applicationFromAnotherProject;

        public Application(IApplication2 application2, ILogger <Application> logger, IBottle bottle,
            ITable table, IApplicationFromAnotherProject applicationFromAnotherProject)
        {

            _application2 = application2;
            _logger = logger;
            _bottle = bottle;
            _table = table;
            _applicationFromAnotherProject = applicationFromAnotherProject;
        }

        public void Run()
        {
            _logger.LogInformation("Logging from the Application class");
            Console.WriteLine("Hello from the application class");
            _application2.Run();
            _bottle.GetBottle();
            _table.GetTable();
            _applicationFromAnotherProject.Run();


        }
    }
}
