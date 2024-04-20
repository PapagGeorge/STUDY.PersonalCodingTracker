
namespace Application.Interfaces
{
    public interface ICalculatePotentialPayout
    {
        decimal MatchPotentialPayoutCalculator(decimal stake, decimal matchOdds);
    }
}
