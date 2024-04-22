using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMatchRepository
    {
        Match GetMatchById(int matchId);
        bool MatchExists(int matchId);
        void CreateMatch(Match match);
        void ChangeMatchStatus(int matchId, string newStatus);
    }
}
