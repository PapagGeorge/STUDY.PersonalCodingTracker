using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_HandlingShapes
{
    public class Square : IShape
    {
        public float Length { get; set; }

        public float CalculateArea()
        {
            return Length * Length;
        }

    }
}
