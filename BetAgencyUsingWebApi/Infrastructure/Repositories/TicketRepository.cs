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
            try
            {
                var tickets = _context.Tickets.ToList();

                foreach (var ticket in tickets)
                {
                    var ticketBets = ticket.TicketBet.Select(tb => tb.Bet).ToList();
                    var ticketStatus = CalculateTicketStatus(ticketBets, betsLost);

                    
                    ticket.TicketStatus = ticketStatus;
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while updating ticket statuses.", ex);
            }
        }

        private string CalculateTicketStatus(List<Bet> ticketBets, List<Bet> betsLost)
        {
            var anyBetLost = ticketBets.Any(tb => betsLost.Any(bl => bl.BetId == tb.BetId));
            var anyBetWon = ticketBets.Any(tb => !betsLost.Any(bl => bl.BetId == tb.BetId));
            var anyBetPending = ticketBets.Any(tb => tb.BetStatus == "Pending");

            if (anyBetLost)
            {
                return "Lost";
            }
            else if (anyBetWon && !anyBetPending)
            {
                return "Won";
            }
            else
            {
                return "Pending";
            }
        }

    }
}
