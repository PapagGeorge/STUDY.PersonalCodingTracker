using Domain.Entities;

namespace Application.Interfaces
{
    public interface IApplication
    {
        Bet CreateBet(int userId, int matchId, string bettingMarket, decimal stake);
        void CreateTicketWithBets(int userId, List<(int matchId, string bettingMarket, decimal stake)> betsData);
        void CreateMatch(Match match);
        void CreateUser(User user);
        void ApplyResult(int matchId, int homeTeamScore, int awayTeamScore);
        IEnumerable<Match> GetAllMatchesByDateRange(DateTime startingDate, DateTime endingDate);
        Match GetMatchById(int matchId);
        User GetUserById(int userId);
        IEnumerable<User> GetAllUsers();
        Bet GetBetById(int betId);
    }
}
