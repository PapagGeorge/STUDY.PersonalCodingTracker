using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITicketBetRepository
    {
        void CreateTicketBet(TicketBet ticketBet);
    }
}
