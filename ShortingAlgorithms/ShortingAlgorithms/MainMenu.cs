namespace ShortingAlgorithms
{
    public class MainMenu
    {
        public static void DisplayMainMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("-------------Sorting Algorithms-------------");
            Console.WriteLine("Pick an algorithm of you preference! Press numbers 1-5 to submit your choice.");
            Console.WriteLine();
            Console.WriteLine("1.Bubble Sort");
            Console.WriteLine("2.Merge Sort");
            Console.WriteLine("3.Quick Sort");
            Console.WriteLine("4.Insertion Sort");
            Console.WriteLine("5.Selection Sort");
            Console.WriteLine("6.Heap Sort");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("After each sorting, performance info will be provided.");
            Console.ReadKey();
        }
    }
}
