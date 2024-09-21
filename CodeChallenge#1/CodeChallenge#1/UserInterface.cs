using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge_1
{
    public static class UserInterface
    {
        public static Citizen SetCitizenByUser()
        {
            int age;
            Console.WriteLine("Please insert the citizen's age");
            
            if(int.TryParse(Console.ReadLine(), out age))
            {
                Citizen citizen = new Citizen(age);
                return citizen;
            }
            else
            {
                return null;
            }
        }

        public static void PrintResult(Citizen citizen)
        {
            int numberOfTimesCitizenHasVoted = citizen.CalculateTimesCitizenHasVoted();
            Console.WriteLine($"Citizen has voted {numberOfTimesCitizenHasVoted} times");
        }
    }
}
