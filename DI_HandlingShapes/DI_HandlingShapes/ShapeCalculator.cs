using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI_HandlingShapes
{
    public class ShapeCalculator
    {
        private IShape _shape;

        public ShapeCalculator(IShape shape)
        {
            _shape = shape;
        }

        public float CalculateArea()
        {
           return _shape.CalculateArea();
        }
    }
}
