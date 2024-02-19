using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionDemo
{
    public class ComputerPlayer : IPlayer
    {
        Random _rng = new Random();
        public Choice GetChoice()
        {
            Choice p2 = (Choice)_rng.Next(0, 3); 
            return p2;
        }
    }
}
