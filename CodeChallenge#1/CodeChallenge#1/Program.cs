namespace CodeChallenge_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Citizen citizen = UserInterface.SetCitizenByUser();
            UserInterface.PrintResult(citizen);
        }
    }
}
