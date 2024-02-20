using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI.Services
{
    public class Dummy : IDummy
    {
        public void Run()
        {
            Console.WriteLine("This message is from Dummy");
        }
    }
}
