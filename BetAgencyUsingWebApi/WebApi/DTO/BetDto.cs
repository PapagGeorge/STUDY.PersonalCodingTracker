namespace WebApi.DTO
{
    public class BetDto
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
    }
}
