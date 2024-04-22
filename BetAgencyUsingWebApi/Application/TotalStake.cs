using Application.Interfaces;
using Domain.Entities;

namespace Application
{
    public class TotalStake : ITotalStake
    {
        public decimal CalculateTotalBetsStake(List<Bet> listOfBets)
        {
            try
            {
                if (listOfBets.Count == 0 || listOfBets == null)
                {
                    throw new Exception("The List of Bets is null or Empty");
                }

                decimal totalStake = 0;

                foreach(Bet bet in listOfBets)
                {
                    totalStake += bet.Stake;
                }

                return totalStake;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to calculate total stake of ticket bets. {ex.Message}");
            }
        }
    }
}
