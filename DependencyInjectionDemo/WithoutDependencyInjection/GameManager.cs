using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace WithoutDependencyInjection
{
    
    public class GameManager
    {
        Random rnd = new Random();
        
        public RoundResult PlayRound()
        {
            //Player's Choice
            Choice p1;
            do
            {
                Console.WriteLine("Enter your choice: (R)ock, (P)aper or (S)cissors");
                string input = Console.ReadLine().ToUpper();

                if (input == "R")
                {
                    p1 = Choice.Rock;
                    break;

                }
                else if (input == "P")
                {
                    p1 = Choice.Paper;
                    break;
                }
                else if (input == "S")
                {
                    p1 = Choice.Scissors;
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter a valide letter: R, P or S");
                }
                
            }while (true);
            

            //Computer's Choice

            Choice p2 = (Choice)rnd.Next(0, 3);
            Console.WriteLine($"Opponent's choice is {p2.ToString()}");

            if (p1 == p2)
            {
                return RoundResult.Draw;
            }
            if(p1 == Choice.Rock && p2 == Choice.Scissors ||
                p1 == Choice.Scissors && p2 == Choice.Paper ||
                p1 == Choice.Paper && p2 == Choice.Rock)
            {
                return RoundResult.Player1Win;
            }
           
                return RoundResult.Player2Win;

        }

    }
}

public enum Choice
{
    Rock,
    Paper,
    Scissors
}

public enum RoundResult
{
    Player1Win,
    Player2Win,
    Draw
}