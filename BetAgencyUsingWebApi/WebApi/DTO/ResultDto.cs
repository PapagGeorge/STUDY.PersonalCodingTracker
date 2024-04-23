namespace WebApi.DTO
{
    public class ResultDto
    {
        public int ResultId { get; set; }
        public DateTime ResultDateTime { get; set; }
        public int MatchId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public string GameResult { get; set; }
        public string OverUnderResult { get; set; }
    }
}
