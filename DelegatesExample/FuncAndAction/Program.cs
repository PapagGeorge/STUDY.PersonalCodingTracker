using System.Threading.Channels;

namespace FuncAndAction
{

    
    public class Program
    {
        static void Main(string[] args)
        {
            Func<int> returnNum = Return10;
            returnNum += Return20;
            returnNum += Return30;
            Func<int, int, bool> evenNum = IsSumEven;
            Action<string> message = Message;

            List<int> rValues = GetAllReturnValues<int>(returnNum).ToList();
            foreach (int i in rValues) { Console.WriteLine(i); }
        }

        static IEnumerable<T> GetAllReturnValues<T>(Func<T> d)
        {
            
            foreach(Func<T> me in d.GetInvocationList())
            yield return me();
        }

        static int Return10() { return 10; }
        static int Return20() { return 20; }
        static int Return30() { return 30; }
        static bool IsSumEven(int a, int b)
        {
            int result = a + b;
            if(result % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
                
        }

        static void Message (string message)
        {
            Console.WriteLine($"My message: {message}");
        }

       

    }
}
