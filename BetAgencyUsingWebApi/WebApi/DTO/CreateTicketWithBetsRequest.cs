
namespace WebApi.DTO
{
    public class CreateTicketWithBetsRequest
    {
        public int UserId { get; set; }
        public List<BetData> BetsData { get; set; }
    }
}
