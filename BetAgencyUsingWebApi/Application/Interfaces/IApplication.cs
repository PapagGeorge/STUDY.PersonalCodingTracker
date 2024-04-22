using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplication
    {
        Bet CreateBet(int userId, int matchId, string bettingMarket, decimal stake);
        void CreateTicketWithBets(int userId, List<(int matchId, string bettingMarket, decimal stake)> betsData);
        void CreateMatch(Match match);
        void CreateUser(User user);
        void AddMatchResult(Result result);
        void ApplyResult(int matchId, int homeTeamScore, int awayTeamScore);
    }
}
