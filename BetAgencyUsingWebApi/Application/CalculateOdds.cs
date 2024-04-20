using Application.Interfaces;

namespace Application
{
    public class CalculateOdds : ICalculateOdds
    {
        private readonly IMatchRepository _matchRepository;

        public CalculateOdds(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public decimal MatchOddsCalculator(int matchId, string bettingMarket)
        {
            try
            {

                if (!_matchRepository.MatchExists(matchId))
                {
                    throw new Exception($"Match with Id: {matchId} does not exist");
                }
                var match = _matchRepository.GetMatchById(matchId);

                decimal betOdds = 0;


                if (bettingMarket == "Home")
                {
                    betOdds = match.HomeTeamWinsOdds;
                }

                else if (bettingMarket == "Draw")
                {
                    betOdds = match.DrawOdds;
                }

                else if (bettingMarket == "Away")
                {
                    betOdds = match.AwayTeamWinsOdds;
                }

                else if (bettingMarket == "Over")
                {
                    betOdds = match.OverOdds;
                }

                else if (bettingMarket == "Under")
                {
                    betOdds = match.UnderOdds;
                }

                else
                {
                    throw new Exception("Betting Market input is wrong");
                }

                return betOdds;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to calculate odds for match with Id: {matchId}. {ex.Message}");
            }
        }
    }
}
