using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_ModuleExample.Services
{
    public class Bottle : IBottle
    {
        public void GetBottle()
        {
            Console.WriteLine("this is a bottle");
        }
    }
}
