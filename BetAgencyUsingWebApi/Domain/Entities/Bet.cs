namespace Domain.Entities
{
    public class Bet
    {
        public int BetId { get; set; }
        public DateTime BetDateTime { get; set; }
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public string BettingMarket { get; set; }
        public decimal Stake { get; set; }
        public decimal BetOdds { get; set; }
        public decimal BetPotentialPayout { get; set; }
        public string BetStatus { get; set; }
        public User User { get; set; }
        public Match Match { get; set; }
        public ICollection<TicketBet> TicketBet { get; set; }


    }
}
