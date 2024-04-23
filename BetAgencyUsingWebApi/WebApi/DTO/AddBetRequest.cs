namespace WebApi.DTO
{
    public class AddBetRequest
    {
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public string BettingMarket { get; set; }
        public decimal Stake { get; set; }
    }
}
