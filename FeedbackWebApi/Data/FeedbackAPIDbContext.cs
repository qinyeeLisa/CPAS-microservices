using Microsoft.EntityFrameworkCore;
using FeedbackWebApi.Models;
using UserWebApi.Models;

namespace FeedbackWebApi.Data
{
    public class FeedbackAPIDbContext : DbContext
    {
        public FeedbackAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Feedback> Feedback { get; set; }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the foreign key relationship between Permit and User
            modelBuilder.Entity<Feedback>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Optional: Specify the delete behavior

            // Additional configurations or constraints can be added here
        }
    }
}
