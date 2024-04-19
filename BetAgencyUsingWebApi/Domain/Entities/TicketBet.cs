namespace Domain.Entities
{
    public class TicketBet
    {
        public int TicketBetId { get; set; }
        public int TicketId { get; set; }
        public int BetId { get; set; }
        public Ticket Ticket { get; set; }
        public Bet Bet { get; set; }
    }
}
