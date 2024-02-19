namespace WithoutDependencyInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();
            string userChoice;
            do
            {
                RoundResult result = gm.PlayRound();

                if (result == RoundResult.Player1Win)
                {
                    Console.WriteLine("You win!");
                }
                else if (result == RoundResult.Player2Win)
                {
                    Console.WriteLine("Opponent wins!");
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                }
                Console.WriteLine("Press (Y)es if you want to continue");
                userChoice = Console.ReadLine().ToUpper();

            } while (userChoice == "Y");

        }
    }
}
