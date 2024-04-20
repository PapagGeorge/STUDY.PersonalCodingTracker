using Application.Interfaces;

namespace Application
{
    public class CalculatePotentialPayout : ICalculatePotentialPayout
    {
        public decimal MatchPotentialPayoutCalculator(decimal stake, decimal matchOdds)
        {
            try
            {
                if(stake <= 0 )
                {
                    throw new Exception("Stake is equal or less than zero");
                }

                if (matchOdds <= 0)
                {
                    throw new Exception("Match Odds is equal or less than zero");
                }

                return stake * matchOdds;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while calculating Potential Payout for match bet. {ex.Message}");
            }
        }
    }
}
