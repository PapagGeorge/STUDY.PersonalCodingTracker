using ShortingAlgorithms.Algorithms;

namespace ShortingAlgorithms
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string menuChoice;

            do
            {
                int userInput = MainMenu.DisplayMainMenu();

                switch (userInput)
                {
                    case 1:
                        menuChoice = Sorter.SortingRobot(BubbleSort.BubbleSortNumbers);
                        break;

                    default:
                        Console.WriteLine("Invalid choice, please select again.");
                        menuChoice = "MENU";
                        break;

                }
            }
            while (menuChoice == "MENU");
            
        }
    }
}
