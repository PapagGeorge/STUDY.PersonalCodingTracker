using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ConfigureServicesDI
{
    public class Application : IApplication
    {
        private readonly IDummy _dummy;

        public Application(IDummy dummy)
        {
            _dummy = dummy;
        }


        public void Run()
        {
            Console.WriteLine("Application Run");
        }

        public void DoSomething()
        {
            _dummy.DoSomething();
        }

    }
}
