using Microsoft.EntityFrameworkCore;
using FeedbackWebApi.Models;

namespace FeedbackWebApi.Data
{
    public class FeedbackAPIDbContext : DbContext
    {
        public FeedbackAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Feedback> Feedback { get; set; }

    }
}
