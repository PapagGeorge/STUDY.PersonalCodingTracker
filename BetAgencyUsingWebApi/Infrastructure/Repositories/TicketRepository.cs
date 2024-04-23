using Application.Interfaces;
using Domain.Entities;
using System.Linq;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public void UpdateTicketStatusWithId(int ticketId, string status)
        {
            try
            {
                var ticket = _context.Tickets.FirstOrDefault(ticket => ticket.TicketId == ticketId);
                ticket.TicketStatus = status;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating changing status of ticket with Id: {ticketId}. {ex.Message}");
            }
        }

        public void UpdateTicketStatusWithBetList(List<Bet> betsLost)
        {
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    {
                        var ticketIdsWithLostBets = _context.TicketBet.ToList()
                        .Where(ticket => betsLost.Any(bet => bet.BetId == ticket.BetId))
                        .Select(ticket => ticket.TicketId)
                        .Distinct();

                        foreach (var ticketId in ticketIdsWithLostBets)
                        {
                            UpdateTicketStatusWithId(ticketId, "Lost");
                        }

                        var ticketIdsWithWonBets = _context.TicketBet.ToList()
                        .Where(ticket => !ticketIdsWithLostBets.Contains(ticket.TicketId))
                        .Select(ticket => ticket.TicketId)
                        .Distinct();

                        foreach (var ticketId in ticketIdsWithWonBets)
                        {
                            UpdateTicketStatusWithId(ticketId, "Won");
                        }
                        
                        transaction.Commit();
                    }

                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new Exception($"An error occured while creating changing status of tickets with bet list. {ex.Message}");
                }
            }
            
            
            
        }
    }
}
