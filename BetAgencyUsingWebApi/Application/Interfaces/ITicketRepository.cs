using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITicketRepository
    {
        void CreateTicket(Ticket ticket);
    }
}
