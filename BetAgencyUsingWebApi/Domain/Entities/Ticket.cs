﻿namespace Domain.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public DateTime TicketDateTime { get; set; }
        public int UserId { get; set; }
        public string TicketStatus { get; set; }
        public decimal TotalStake { get; set; }
        public decimal PotentialPayout { get; set; }
        public User User { get; set; }
        public ICollection<TicketBet> TicketBet { get; set; }
    }
}
