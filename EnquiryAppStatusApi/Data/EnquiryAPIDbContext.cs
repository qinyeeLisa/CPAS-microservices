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
        
    }
}
