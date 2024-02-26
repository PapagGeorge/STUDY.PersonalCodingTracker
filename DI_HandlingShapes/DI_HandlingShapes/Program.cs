namespace DI_HandlingShapes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IShape triangle = new Triangle { Base = 2, Height = 2 };
            IShape circle = new Circle { Radius = 2};
            IShape square = new Square { Length = 2 };

            ShapeCalculator calc1 = new ShapeCalculator(triangle);
            ShapeCalculator calc2 = new ShapeCalculator(circle);
            ShapeCalculator calc3 = new ShapeCalculator(square);

            Console.WriteLine(calc1.CalculateArea());
            Console.WriteLine(calc2.CalculateArea());
            Console.WriteLine(calc3.CalculateArea());

        }
    }
}
