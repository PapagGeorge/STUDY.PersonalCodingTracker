using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITicketRepository
    {
        void CreateTicket(Ticket ticket);
        void UpdateTicketStatusWithBetList(IEnumerable<Bet> betsLost);
        void UpdateTicketStatusWithId(int ticketId, string status);


    }
}
