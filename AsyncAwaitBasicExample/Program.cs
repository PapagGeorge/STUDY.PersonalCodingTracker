using System.Diagnostics;

namespace AsyncAwaitBasicExample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's Cook Breakfast!");
            var sw = new Stopwatch();
            sw.Start();



            sw.Stop();
            double seconds = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("Breakfast is Ready!");
            Console.WriteLine($"It took {seconds} to make breakfast");
        }

        static Coffee AddCoffeeIngredients()
        {
            Console.WriteLine("Started coffee making Machine...");
            Task.Delay(1000).Wait();
            Console.WriteLine("Coffee is ready!");
            return new Coffee();
        }

        static void HeatPan()
        {
            Console.WriteLine("Started heating the Pan");
            Task.Delay(2000).Wait();
            Console.WriteLine("The pan is Hot!");
        }

        static Eggs FryEggs()
        {
            Console.WriteLine("Started Frying Eggs...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Eggs are ready!");
            return new Eggs();
        }

        static Bacon FryBacon()
        {
            Console.WriteLine("Started Frying Bacon...");
            Task.Delay(6000).Wait();
            Console.WriteLine("Bacon Ready!");
            return new Bacon();
        }

        static Toast MakeToast()
        {
            Console.WriteLine("Started Making Toast...");
            Task.Delay(2000).Wait();
            Console.WriteLine("Toast is Ready!");
            return new Toast();
        }

        static void AddButterToToast()
        {
            Console.WriteLine("Adding Butter to Toast");
            Console.WriteLine("Butter Added to Toast");
        }

        static void AddJamToToast()
        {
            Console.WriteLine("Adding Jam to Toast");
            Console.WriteLine("Jam Added to Toast");
        }

    }

    internal class Coffee
    {

    }

    internal class Eggs
    {

    }

    internal class Bacon
    {

    }

    internal class Toast
    {

    }
}


