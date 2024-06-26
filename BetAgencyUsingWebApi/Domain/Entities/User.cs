﻿namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public ICollection<Bet> Bets { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
