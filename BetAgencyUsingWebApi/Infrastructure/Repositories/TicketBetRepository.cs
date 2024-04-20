using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class TicketBetRepository : ITicketBetRepository
    {
        private readonly BetDbContext _context;

        public TicketBetRepository(BetDbContext context)
        {
            _context = context;
        }
        public void CreateTicketBet(TicketBet ticketBet)
        {
            try
            {
                if(ticketBet == null)
                {
                    throw new Exception("TicketBet object is null");
                }

                _context.TicketBet.Add(ticketBet);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occure while creating new TicketBet record. {ex.Message}");
            }
        }
    }
}
