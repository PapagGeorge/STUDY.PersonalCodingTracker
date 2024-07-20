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
                        menuChoice = SortingHandler.PickAlgorithm(SortingFunctions.BubbleSortNumbers);
                        break;
                    case 2:
                        menuChoice = SortingHandler.PickAlgorithm(SortingFunctions.MergeSortNumbers);
                        break;
                    case 3:
                        menuChoice = SortingHandler.PickAlgorithm(SortingFunctions.QuickSortNumbers);
                        break;
                    case 4:
                        menuChoice = SortingHandler.PickAlgorithm(SortingFunctions.InsertionSortNumbers);
                        break;
                    case 5:
                        menuChoice = SortingHandler.PickAlgorithm(SortingFunctions.SelectionSortNumbers);
                        break;
                    case 6:
                        menuChoice = SortingHandler.PickAlgorithm(SortingFunctions.HeapSortNumbers);
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
