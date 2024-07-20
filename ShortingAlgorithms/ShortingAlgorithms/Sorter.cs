namespace ShortingAlgorithms
{
    public static class Sorter
    {
        public static void SortingRobot(Func<List<int>, List<int>> action)
        {
            try
            {
                Console.Write("Please insert the numbers you want to be sorted separated with commas: ");
                string userInput = Console.ReadLine();

                List<int> usersinputToList = StringToIntListConverter.ConvertStringToIntegerList(userInput);

                if(usersinputToList != null && usersinputToList.Count > 1)
                {
                    List<int> sortedList = action(usersinputToList);

                    Console.WriteLine($"Here are the numbers sorted: {sortedList}");
                }
                else
                {
                    throw new ArgumentException("Your list should not be empty and should contain at least 2 numbers");
                }   
            }
            catch(Exception e)
            {
                throw new Exception("An error occured");
            }
        }
    }
}
