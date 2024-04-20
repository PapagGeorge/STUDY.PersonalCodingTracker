﻿using Application.Interfaces;
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
    }
}
