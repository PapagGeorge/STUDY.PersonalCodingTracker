
using System.ComponentModel.DataAnnotations;

namespace WebApi.DTO
{
    public class CreateTicketWithBetsRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public List<BetData> BetsData { get; set; }
    }
}
