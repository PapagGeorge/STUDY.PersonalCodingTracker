namespace ShortingAlgorithms
{
    public static class SortingHandler
    {
        public static string PickAlgorithm(Func<List<int>, List<int>> action)
        {
            try
            {
                Console.Write("Please insert the numbers you want to be sorted separated with commas: ");
                string userInput = Console.ReadLine();
                string menuOption;

                List<int> usersinputToList = StringToIntListConverter.ConvertStringToIntegerList(userInput);

                if(usersinputToList != null && usersinputToList.Count > 1)
                {
                    List<int> sortedList = action(usersinputToList);

                    Console.WriteLine($"Here are the numbers sorted: {string.Join(",", sortedList)}");
                    Console.WriteLine();
                    Console.Write("Insert \"MENU\" if you would like to try another one. Otherwise you can insert any other key to exit: ");
                    menuOption = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Your list should not be empty and should contain at least 2 numbers");
                    Console.Write("Insert \"MENU\" and press enter if you would like to try another one. Otherwise you can insert any other key to exit: ");
                    menuOption = Console.ReadLine();
                }

                return menuOption;
            }
            catch(Exception e)
            {
                throw new Exception("An error occured");
            }
        }
    }
}
