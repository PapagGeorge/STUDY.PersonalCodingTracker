using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly BetDbContext _context;
        public BetRepository(BetDbContext context)
        {
            _context = context;
        }
        public void AddBet(Bet bet)
        {
            try
            {
                _context.Bets.Add(bet);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while adding new bet. {ex.Message}");
            }
        }
    }
}
