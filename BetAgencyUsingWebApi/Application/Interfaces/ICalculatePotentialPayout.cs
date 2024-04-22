using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICalculatePotentialPayout
    {
        decimal MatchPotentialPayoutCalculator(decimal stake, decimal matchOdds);
        decimal BetListPotentialPayoutCalculator(List <Bet> betList);
    }
}
