using Microsoft.EntityFrameworkCore;
using SearchCampsitesApi.Models;
using UserWebApi.Models;

namespace SearchCampsitesApi.Data
{
    public class CampsiteAPIDbContext : DbContext
    {
        public CampsiteAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Campsites> Campsite { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Campsites>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
