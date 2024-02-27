using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ApplicationFromAnotherProject :IApplicationFromAnotherProject
    {
        public void Run()
        {
            Console.WriteLine("Hello from another project");
        }
    }
}
