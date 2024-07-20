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
                        menuChoice = Sorter.PickAlgorithm(SortingFunctions.BubbleSortNumbers);
                        break;
                    case 2:
                        menuChoice = Sorter.PickAlgorithm(SortingFunctions.MergeSortNumbers);
                        break;
                    case 3:
                        menuChoice = Sorter.PickAlgorithm(SortingFunctions.QuickSortNumbers);
                        break;
                    case 4:
                        menuChoice = Sorter.PickAlgorithm(SortingFunctions.InsertionSortNumbers);
                        break;
                    case 5:
                        menuChoice = Sorter.PickAlgorithm(SortingFunctions.SelectionSortNumbers);
                        break;
                    case 6:
                        menuChoice = Sorter.PickAlgorithm(SortingFunctions.HeapSortNumbers);
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
