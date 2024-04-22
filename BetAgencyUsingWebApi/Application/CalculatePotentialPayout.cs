using Application.Interfaces;
using Domain.Entities;

namespace Application
{
    public class CalculatePotentialPayout : ICalculatePotentialPayout
    {
        public decimal BetListPotentialPayoutCalculator(List<Bet> betList)
        {
            try
            {
                if (betList == null || betList.Count == 0)
                {
                    throw new Exception("The list of bets was null or empty while calculating Total Potential Payout for Ticket");
                }
                decimal betsPotentialPayout = 0;

                foreach(Bet bet in betList)
                {
                    betsPotentialPayout += bet.BetPotentialPayout;
                }

                return betsPotentialPayout;
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured while calculating Total Potential Payout of BetList");
            }
            

        }

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
