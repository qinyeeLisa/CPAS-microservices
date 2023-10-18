using EnquiryAppStatusApi.Models;
using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Models;

namespace EnquiryAppStatusApi.Data
{
    public class EnquiryAPIDbContext : DbContext
    {

        public EnquiryAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<Permit> Permits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Enquiry>()
                .HasOne(p => p.Permit)
                .WithMany()
                .HasForeignKey(p => p.PermitId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
