using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_HandlingShapes
{
    public class Triangle : IShape
    {
        public float Base {  get; set; }
        public float Height {  get; set; }

        public float CalculateArea()
        {
            return 0.5f * Height * Base;
        }
    }
}
