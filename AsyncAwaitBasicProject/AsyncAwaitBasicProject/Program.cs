using System.Diagnostics;

namespace AsyncAwaitBasicExample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Let's Cook Breakfast!");
            var sw = new Stopwatch();
            sw.Start();


            
            var coffeeTask = MakeCoffeeAsync();
            var panFryingTask = DoTheFrying();
            var makeToastTask = MakeToastAsync();

            await Task.WhenAll(coffeeTask, panFryingTask, makeToastTask);


            sw.Stop();
            double seconds = sw.ElapsedMilliseconds / 1000;
            Console.WriteLine("Breakfast is Ready!");
            Console.WriteLine($"It took {seconds} seconds to make breakfast");
        }

        static void AddCoffeeIngredients()
        {
            Console.WriteLine("Adding Ingredients to the Coffee Machine...");
            Console.WriteLine("All ingredients Added!");
        }

        static async Task<Coffee> MakeCoffeeAsync()
        {
            AddCoffeeIngredients();
            Console.WriteLine("Started coffee making Machine...");
            await Task.Delay(1000);
            Console.WriteLine("Coffee is ready!");
            return new Coffee();
        }

        static async Task DoTheFrying()
        {
            await HeatPanAsync();
            var fryingEggsTask = FryEggsAsync();
            var fryingBaconTask = FryBaconAsync();
            await Task.WhenAll(fryingEggsTask, fryingBaconTask);
        }
        static async Task HeatPanAsync()
        {
            Console.WriteLine("Started heating the Pan");
            await Task.Delay(2000);
            Console.WriteLine("The pan is Hot!");
        }

        static async Task<Eggs> FryEggsAsync()
        {
            Console.WriteLine("Started Frying Eggs...");
            await Task.Delay(3000);
            Console.WriteLine("Eggs are ready!");
            return new Eggs();
        }

        static async Task<Bacon> FryBaconAsync()
        {
            Console.WriteLine("Started Frying Bacon...");
            await Task.Delay(6000);
            Console.WriteLine("Bacon Ready!");
            return new Bacon();
        }

        static async Task<Toast> MakeToastAsync()
        {
            Console.WriteLine("Started Making Toast...");
            var toast = new Toast();
            AddButterToToast(toast);
            AddJamToToast(toast);
            await Task.Delay(2000);
            Console.WriteLine("Toast is Ready!");
            return toast;
        }

        static void AddButterToToast(Toast toast)
        {
            Console.WriteLine("Adding Butter to Toast");
            Console.WriteLine("Butter Added to Toast");
        }

        static void AddJamToToast(Toast toast)
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


