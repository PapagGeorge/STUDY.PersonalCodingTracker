using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClassFromAnotherProject : IClassFromAnotherProject
    {
        public void AnotherProject()
        {
            Console.WriteLine("Hello from another project");
        }

    }
}
