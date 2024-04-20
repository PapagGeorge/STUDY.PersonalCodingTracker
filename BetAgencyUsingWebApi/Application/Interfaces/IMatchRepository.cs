using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMatchRepository
    {
        Match GetMatchById(int matchId);
        bool MatchExists(int matchId);
    }
}
