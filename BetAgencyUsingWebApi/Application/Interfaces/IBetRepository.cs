using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBetRepository
    {
        void AddBet(Bet bet);
        void ChangeBetStatus(int betId, string status);
        IEnumerable<Bet> GetBetsByMatchId(int matchId);
        Bet GetBetById(int betId);
        
    }
}
