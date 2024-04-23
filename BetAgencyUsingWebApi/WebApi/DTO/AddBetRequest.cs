using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class AddBetRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int MatchId { get; set; }
        [Required]
        public string BettingMarket { get; set; }
        [Required]
        public decimal Stake { get; set; }
    }
}
