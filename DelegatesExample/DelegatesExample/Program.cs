namespace DelegatesExample

    
{
    public delegate bool MeDelegate(int num);

    public class Program
    {
        static void Main(string[] args)
        {
            List<int> myNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            MeDelegate Less5 = new MeDelegate(isNumLessThanFive);

            List<int> resultNums = CheckNumbersLessThan(myNumbers, Less5);

            foreach (int num in resultNums)
            {
                Console.WriteLine(num);
            }

        }


        public static bool isNumLessThanFive(int x) { return x < 5; }
        public static bool isNumLessThanThree(int x) { return x < 3; }
        public static bool isNumLessThanSix(int x) { return x < 6; }
        public static bool isNumLessThanEight(int x) { return x < 8; }




        public static List<int> CheckNumbersLessThan(List<int> numbers, MeDelegate isLessThan)
        {
            List <int> resultList = new List <int> ();
            foreach (int number in numbers)
            {
                if (isLessThan(number))
                {
                    resultList.Add (number);
                }
                
            }
            return resultList;
        }

    }
    
}
