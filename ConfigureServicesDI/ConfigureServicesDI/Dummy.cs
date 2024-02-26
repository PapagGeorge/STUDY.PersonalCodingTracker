using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigureServicesDI
{
    public class Dummy : IDummy
    {
        public void DoSomething()
        {
            Console.WriteLine("Do Something invoked");
        }
    }
}
