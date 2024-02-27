using Microsoft.Extensions.Logging;
using ClassLibrary1;
using ClassLibrary_ModuleExample.Services;

namespace DI_Practise.Services
{
    public class Application : IApplication, IApplication2
    {
        private readonly ILogger<Application> _logger;
        private readonly IApplication2 _application2;
        private readonly IClassFromAnotherProject _classFromAnotherProject;
        private readonly IBottle _bottle;
        private readonly ITable _table;

        public Application(ILogger<Application> logger, IApplication2 application2, IClassFromAnotherProject classFromAnotherProject,
            IBottle bottle, ITable table)
        {
            _logger = logger;
            _application2 = application2;
            _classFromAnotherProject = classFromAnotherProject;
            _bottle = bottle;
            _table = table;
        }

        public void Run()
        {
            _logger.LogInformation("Logging from the ApplicationClass");
            _application2.Run();
            _classFromAnotherProject.AnotherProject();
            _bottle.GetBottle();
            _table.GetTable();

        }
       
    }
}
