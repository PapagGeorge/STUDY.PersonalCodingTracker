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
        private readonly ITotalStake _totalStake;
        private readonly ITicketRepository _ticketRepository;
        private readonly IResultRepository _resultRepository;

        public Application(IMatchRepository matchRepository, IBetRepository betRepository,
            IUserRepository userRepository, ICalculateOdds calculateOdds, ICalculatePotentialPayout potentialPayout, ITotalStake totalStake
            , ITicketRepository ticketRepository, IResultRepository resultRepository)
        {
            _betRepository = betRepository;
            _matchRepository = matchRepository;
            _userRepository = userRepository;
            _calculateOdds = calculateOdds;
            _potentialPayout = potentialPayout;
            _totalStake = totalStake;
            _ticketRepository = ticketRepository;
            _resultRepository = resultRepository;
        }
        public Bet CreateBet(int userId, int matchId, string bettingMarket, decimal stake)
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
                    BetOdds = Math.Round(betOdds, 2),
                    BetPotentialPayout = betPotentialPayout,
                    BetStatus = "Pending"
                };

                _betRepository.AddBet(newBet);

                return newBet;

                


            }
            catch(Exception ex)
            {
                throw new Exception($"An error occured while creating new Bet for User with Id: {userId}. {ex.Message}");
            }
        }

        public void CreateTicketWithBets(int userId, List<(int matchId, string bettingMarket, decimal stake)> betsData)
        {
            try
            {
                List<Bet> betList = new List<Bet>();

                foreach(var betData in betsData)
                {
                    int matchId = betData.matchId;
                    string bettingMarket = betData.bettingMarket;
                    decimal stake = Math.Round(betData.stake, 2);

                    var bet = CreateBet(userId, betData.matchId, betData.bettingMarket, betData.stake);

                    betList.Add(bet);
                }

                var ticket = new Ticket()
                {
                    UserId = userId,
                    TicketDateTime = DateTime.Now,
                    TicketStatus = "Pending",
                    TotalStake = Math.Round(_totalStake.CalculateTotalBetsStake(betList), 2),
                    PotentialPayout = _potentialPayout.BetListPotentialPayoutCalculator(betList)
                };

                _ticketRepository.CreateTicket(ticket);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while creating new Ticket with bet list for User with Id: {userId}. {ex.Message}");
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
                _matchRepository.CreateMatch(match);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to create a new match. {ex.Message}");
            }
        }

        public void CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new Exception("User you are trying to create is null");
                }
                _userRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to create a new user. {ex.Message}");
            }
        }

        public void AddMatchResult(Result result)
        {
            try
            {
                if (result == null)
                {
                    throw new Exception("Result you are trying to create is null");
                }

                result.ResultDateTime = DateTime.Now;

                _resultRepository.AddMatchResult(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to add a result. {ex.Message}");
            }
        }

        public void ApplyResult(int matchId, int homeTeamScore, int awayTeamScore)
        {
            try
            {
                _matchRepository.ChangeMatchStatus(matchId, "Finished");

                string gameResult;

                if (homeTeamScore > awayTeamScore)
                {
                    gameResult = "Home";
                }

                else if (homeTeamScore < awayTeamScore)
                {
                    gameResult = "Away";
                }

                else
                {
                    gameResult = "Draw";
                }

                string overUnderResult;

                if (homeTeamScore + awayTeamScore <= 2)
                {
                    overUnderResult = "Under";
                }
                else
                {
                    overUnderResult = "Over";
                }



                Result result = new Result()
                {
                    ResultDateTime = DateTime.Now,
                    MatchId = matchId,
                    HomeTeamScore = homeTeamScore,
                    AwayTeamScore = awayTeamScore,
                    OverUnderResult = overUnderResult,
                    GameResult = gameResult
                };

                _resultRepository.AddMatchResult(result);

                var betsForThisMatch = _betRepository.GetBetsByMatchId(matchId);

                
                List<Bet> betsLost = new List<Bet>();


                foreach (var bet in betsForThisMatch)
                {
                    if (bet.BettingMarket == overUnderResult || bet.BettingMarket == gameResult)
                    {
                        _betRepository.ChangeBetStatus(bet.BetId, "Won");
                        
                    }

                    else
                    {
                        _betRepository.ChangeBetStatus(bet.BetId, "Lost");
                        betsLost.Add(bet);
                    }
                }

                _ticketRepository.UpdateTicketStatusWithBetList(betsLost);

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to apply results to Match, Bets and Tickets. {ex.Message}");
            }
        }

        public IEnumerable<Match> GetAllMatchesByDateRange(DateTime startingDate, DateTime endingDate)
        {
            try
            {
                var matches = _matchRepository.GetAllMatchesByDateRange(startingDate, endingDate);
                return matches;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get matches in a specific range of dates. {ex.Message}");
            }
        }

        public Match GetMatchById(int matchId)
        {
            try
            {
                var match = _matchRepository.GetMatchById(matchId);
                return match;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get match with Id: {matchId}. {ex.Message}");
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                var user = _userRepository.GetUserById(userId);
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get get user with Id: {userId}. {ex.Message}");
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                var users = _userRepository.GetAllUsers();
                return users;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occured while trying to get get all existing users. {ex.Message}");
            }
        }
    }
}
