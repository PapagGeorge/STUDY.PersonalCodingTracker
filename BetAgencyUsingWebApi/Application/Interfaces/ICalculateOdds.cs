using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICalculateOdds
    {
        decimal MatchOddsCalculator(int matchId, string bettingMarket);
    }
}
