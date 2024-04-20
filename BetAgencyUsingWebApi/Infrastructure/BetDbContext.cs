using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;


namespace Infrastructure
{
    public class BetDbContext : DbContext
    {
        public BetDbContext(DbContextOptions<BetDbContext> options) : base(options)
        {
            
        }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketBet> TicketBet { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
        //    optionsBuilder.UseSqlServer(dbConfiguration.ConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketBet>()
               .HasKey(tb => new { tb.TicketId, tb.BetId });

            modelBuilder.Entity<TicketBet>()
                        .HasOne(tb => tb.Ticket)
                        .WithMany(t => t.TicketBet)
                        .HasForeignKey(tb => tb.TicketId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TicketBet>()
                        .HasOne(tb => tb.Bet)
                        .WithMany(b => b.TicketBet)
                        .HasForeignKey(tb => tb.BetId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bet>()
                        .Property(b => b.BetDateTime)
                        .IsRequired().HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Bet>()
                        .Property(b => b.UserId)
                        .IsRequired();

            modelBuilder.Entity<Bet>()
                        .Property(b => b.MatchId)
                        .IsRequired();

            modelBuilder.Entity<Bet>()
                        .Property(b => b.BettingMarket)
                        .IsRequired();

            modelBuilder.Entity<Bet>()
                        .Property(b => b.Stake)
                        .IsRequired();

            modelBuilder.Entity<Bet>()
                        .Property(b => b.BetOdds)
                        .IsRequired();

            modelBuilder.Entity<Bet>()
                        .Property(b => b.BetPotentialPayout)
                        .IsRequired();

            modelBuilder.Entity<Bet>()
                        .Property(b => b.BetStatus)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.MatchDateTime)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.HomeTeam)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.AwayTeam)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.Status)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.HomeTeamWinsOdds)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.AwayTeamWinsOdds)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.DrawOdds)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.OverOdds)
                        .IsRequired();

            modelBuilder.Entity<Match>()
                        .Property(m => m.UnderOdds)
                        .IsRequired();

            modelBuilder.Entity<Result>()
                        .Property(r => r.ResultDateTime)
                        .IsRequired().HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Result>()
                        .Property(r => r.MatchId)
                        .IsRequired();

            modelBuilder.Entity<Result>()
                        .Property(r => r.HomeTeamScore)
                        .IsRequired();

            modelBuilder.Entity<Result>()
                        .Property(r => r.AwayTeamScore)
                        .IsRequired();

            modelBuilder.Entity<Ticket>()
                        .Property(t => t.TicketDateTime)
                        .IsRequired().HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Ticket>()
                        .Property(t => t.UserId)
                        .IsRequired();

            modelBuilder.Entity<Ticket>()
                        .Property(t => t.TicketStatus)
                        .IsRequired();

            modelBuilder.Entity<Ticket>()
                        .Property(t => t.TotalStake)
                        .IsRequired();

            modelBuilder.Entity<Ticket>()
                        .Property(t => t.PotentialPayout)
                        .IsRequired();

            modelBuilder.Entity<TicketBet>()
                        .Property(t => t.TicketId)
                        .IsRequired();

            modelBuilder.Entity<TicketBet>()
                        .Property(t => t.BetId)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(u => u.FullName)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(u => u.Mobile)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(u => u.Email)
                        .IsRequired();

        }

    }
}
