namespace DependencyInjectionDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
           GameManager gm = new GameManager(new HumanPlayer(), new ComputerPlayer());

            do
            {
                RoundResult result = gm.Playground();

                if (result == RoundResult.Player1Win)
                {
                    Console.WriteLine("Player 1 Wins!");
                }
                else if (result == RoundResult.Player2Win)
                {
                    Console.WriteLine("Player 2 Wins!");
                }
                else
                {
                    Console.WriteLine("It's a draw!");
                }
                Console.Write("Play Again (Y/N)");

            }
            while (Console.ReadLine() == "Y");
        }
    }
}
