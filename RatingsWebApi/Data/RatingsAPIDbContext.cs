using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RatingsWebApi.Models;
using UserWebApi.Models;

namespace RatingsWebApi.Data
{
    public class RatingsAPIDbContext : DbContext
    {
        public RatingsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ratings> Rating { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Ratings>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
