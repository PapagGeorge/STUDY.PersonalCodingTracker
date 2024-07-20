namespace ShortingAlgorithms
{
    public class StringToIntListConverter
    {
        static List<int> ConvertStringToIntegerList(string str)
        {
            string[] strArray = str.Replace(" ", "").Split(",");
            List<int> integerList = new List<int>();

            try
            {
                foreach (string element in strArray)
                {
                    int number = 0;

                    if (int.TryParse(element, out number))
                    {
                        integerList.Add(number);
                    }
                    else
                    {
                        throw new ArgumentException("You should only insert numbers separated with commas");
                    }
                }

                return integerList;
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured");
            }
        }
    }
}
