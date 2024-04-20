using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly BetDbContext _context;
        public TicketRepository(BetDbContext context)
        {
            _context = context;
        }
        public void CreateTicket(Ticket ticket)
        {
            try
            {
                if(ticket == null)
                {
                    throw new Exception("Ticket object is null");
                }
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new ticket. {ex.Message}");
            }
        }
    }
}
