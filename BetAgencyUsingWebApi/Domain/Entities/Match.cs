namespace Domain.Entities
{
    public class Match
    {
        public int MatchId { get; set; }
        public DateTime MatchDateTime { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Status { get; set; }
        public decimal HomeTeamWinsOdds { get; set; }
        public decimal AwayTeamWinsOdds { get; set; }
        public decimal DrawOdds { get; set; }
        public decimal OverOdds { get; set; }
        public decimal UnderOdds { get; set; }
        public Result Result { get; set; }
        public ICollection<Bet> Bets { get; set; }
    }
}
