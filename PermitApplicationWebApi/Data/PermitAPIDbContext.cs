using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Models;
using System.Collections.Generic;
using UserWebApi.Models;

namespace PermitApplicationWebApi.Data
{
    public class PermitAPIDbContext : DbContext
    {

        public PermitAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Permit> Permits { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Permit>()  
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        
    }
}
