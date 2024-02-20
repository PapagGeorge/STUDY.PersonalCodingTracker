using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Services
{
    internal class Application : IApplication
    {
        private IDummy _dummy;

        public Application(IDummy dummy)
        {
            _dummy = dummy;
        }
        public void Run()
        {
            _dummy.Run();
            Console.WriteLine("Application method called.");
        }
    }
}
