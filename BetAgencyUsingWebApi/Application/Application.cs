using Application.Interfaces;
using Domain.Entities;

namespace Application
{
    public class Application : IApplication
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IBetRepository _betRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICalculateOdds _calculateOdds;
        private readonly ICalculatePotentialPayout _potentialPayout;

        public Application(IMatchRepository matchRepository, IBetRepository betRepository,
            IUserRepository userRepository, ICalculateOdds calculateOdds, ICalculatePotentialPayout potentialPayout)
        {
            _betRepository = betRepository;
            _matchRepository = matchRepository;
            _userRepository = userRepository;
            _calculateOdds = calculateOdds;
            _potentialPayout = potentialPayout;
        }
        public void CreateBet(int userId, int matchId, string bettingMarket, decimal stake)
        {
            try
            {
                if (!_matchRepository.MatchExists(matchId))
                {
                    throw new Exception($"Match with Id: {matchId} does not exist");
                }

                if (!_userRepository.UserExists(userId))
                {
                    throw new Exception($"User with Id: {userId} does not exist");
                }

                decimal betOdds = _calculateOdds.MatchOddsCalculator(matchId, bettingMarket);
                decimal betPotentialPayout = _potentialPayout.MatchPotentialPayoutCalculator(stake, betOdds);

                if (betOdds <= 0)
                {
                    throw new Exception("An error occured. Match betting odds returned zero.");
                }

                if (betPotentialPayout <= 0)
                {
                    throw new Exception("An error occured. Bet Potential Payout returned zero.");
                }


                var newBet = new Bet()
                {
                    UserId = userId,
                    BetDateTime = DateTime.Now,
                    MatchId = matchId,
                    BettingMarket = bettingMarket,
                    Stake = stake,
                    BetOdds = betOdds,
                    BetPotentialPayout = betPotentialPayout,
                    BetStatus = "Pending"
                };

                _betRepository.AddBet(newBet);


            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while creating new Bet for User with Id: {userId}. {ex.Message}");
            }
        }
    }
}
