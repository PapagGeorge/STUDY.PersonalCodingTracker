using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class MatchDto
    {
        public int MatchId { get; set; }
        [Required]
        public DateTime MatchDateTime { get; set; }
        [Required]
        public string HomeTeam { get; set; }
        [Required]
        public string AwayTeam { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public decimal HomeTeamWinsOdds { get; set; }
        [Required]
        public decimal AwayTeamWinsOdds { get; set; }
        [Required]
        public decimal DrawOdds { get; set; }
        [Required]
        public decimal OverOdds { get; set; }
        [Required]
        public decimal UnderOdds { get; set; }
        
    }
}
