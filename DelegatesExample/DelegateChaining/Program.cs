namespace DelegateChaining



{

    delegate void MeDelegate();
    public class Program
    {
        static void Main(string[] args)
        {
            MeDelegate d = new MeDelegate(Foo);
            d += Goo;
            d += Sue;
            d += Foo;
            d -= Sue;
            d();
        }

        public static void Foo() { Console.WriteLine("Foo"); }
        public static void Goo() { Console.WriteLine("Goo"); }
        public static void Sue() { Console.WriteLine("Sue"); }

    }
}
