using Application.Interfaces;

namespace PresentationLayer
{
    public class AppScreen
    {
        private readonly IApplication _application;

        public AppScreen(IApplication application)
        {
           _application = application;
        }

        internal static void Welcome()
        {
            Console.Clear();
            Console.Title = "BookingApp";
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n\n-----------------Welcome to BookingApp-----------------\n\n");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("-----------------BookingApp Menu-----------------\n");
            Console.WriteLine("1. Show All Destinations");
            Console.WriteLine("2. Show Trip Choices For Each Destination");
            Console.WriteLine("3. Book a Trip");
            Console.WriteLine("4. Pay Invoice");
            Console.WriteLine("5. View Top Choices By Our Customers");
            Console.WriteLine("6. Exit");

        }

        private int GetMenuChoice()
        {

            while (true)
            {
                Console.WriteLine("\n\nPlease enter your choice 1 - 6 and press ENTER");
                if (int.TryParse(Console.ReadLine(), out var choice))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        private void RunMenuChoice()
        {
            Welcome();
            while (true)
            {
                DisplayMenu();
                var choice = GetMenuChoice();

                switch(choice)
                {
                    case 1:
                        
                        Console.WriteLine("\n\n\n-----All Destinations-----\n\n");
                        _application.ShowAllDestinations();
                        Console.WriteLine("Press any key to return to the Menu");
                        Console.ReadKey();
                        break;


                    case 2:



                        Console.WriteLine("\n\nBased on the destination you chose there are the following travelling choices at the moment: ");



                        break;
                }
            }


        }
    }
}
