using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBetRepository
    {
        void AddBet(Bet bet);
    }
}
