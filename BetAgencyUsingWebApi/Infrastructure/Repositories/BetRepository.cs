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

        public void ChangeBetStatus(int betId, string status)
        {
            try
            {
                var bet = _context.Bets.FirstOrDefault(bet => bet.UserId == betId);
                bet.BetStatus = status;
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while changing bet status. {ex.Message}");
            }
        }

        public IEnumerable<Bet> GetBetsByMatchId(int matchId)
        {
            try
            {
                var betsById = _context.Bets.Where(bet => bet.MatchId == matchId).ToList();
                return betsById;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while searching bets with Match Id: {matchId}. {ex.Message}");
            }

        }
    }
}
