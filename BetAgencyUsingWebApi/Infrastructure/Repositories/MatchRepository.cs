using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly BetDbContext _context;
        public MatchRepository(BetDbContext context)
        {
            _context = context;
        }
        public Match GetMatchById(int matchId)
        {
            try
            {
                var match = _context.Matches.FirstOrDefault(match => match.MatchId == matchId);

                if (match == null)
                {
                    throw new Exception("Match object is null");
                }
                return match;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find match with Id: {matchId}. {ex.Message}");
            }
        }

        public bool MatchExists(int matchId)
        {
            try
            {
                return _context.Matches.Any(match => match.MatchId == matchId);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find if match with Id: {matchId} exists. {ex.Message}");
            }

        }

        public void CreateMatch(Match match)
        {
            try
            {
                if(match == null)
                {
                    throw new Exception("Match you are trying to create is null");
                }

                _context.Matches.Add(match);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to create a new match. {ex.Message}");
            }
        }
        public void ChangeMatchStatus(int matchId, string newStatus)
        {
            try
            {
                var match = _context.Matches.FirstOrDefault(match => match.MatchId == matchId);
                match.Status = newStatus;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to update status of a match. {ex.Message}");
            }
        }

        public IEnumerable<Match> GetAllMatchesByDateRange(DateTime startingDate, DateTime endingDate)
        {
            try
            {
                var matches = _context.Matches.Where(match => match.MatchDateTime >= startingDate && match.MatchDateTime <= endingDate);
                return matches;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to find matches in a set range of dates. {ex.Message}");
            }
        }
    }
}
