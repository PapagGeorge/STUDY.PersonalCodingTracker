﻿using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using System.Configuration;

namespace Infrastructure
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base (options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var dbConfiguration = ((DatabaseConfiguration)ConfigurationManager.GetSection("DatabaseConfigurationSection"));
            optionsBuilder.UseSqlServer("Data Source=DESKTOP;Database=NewLibrary;Integrated Security=SSPI;Trust Server Certificate=True;");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
            
        //    modelBuilder.Entity<Transaction>()
        //        .HasOne(t => t.Member)
        //        .WithMany(m => m.Transactions)
        //        .HasForeignKey(t => t.MemberId);

        //    modelBuilder.Entity<Transaction>()
        //        .HasOne(t => t.Book)
        //        .WithMany(b => b.Transactions)
        //        .HasForeignKey(t => t.BookId);
        //}

    }
}
