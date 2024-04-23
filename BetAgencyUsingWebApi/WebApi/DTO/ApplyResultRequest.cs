using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class ApplyResultRequest
    {
        [Required]
        public int MatchId { get; set; }
        [Required]
        public int HomeTeamScore { get; set; }
        [Required]
        public int AwayTeamScore { get; set; }
    }
}
