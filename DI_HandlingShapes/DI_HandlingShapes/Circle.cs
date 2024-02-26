using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_HandlingShapes
{
    public class Circle : IShape
    {
        public float Radius { get; set; }

        public float CalculateArea()
        {
            return (float)Math.PI * Radius * Radius;
        }


    }
}
