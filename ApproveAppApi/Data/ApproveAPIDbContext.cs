using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Data;
using PermitApplicationWebApi.Models;

namespace ApproveAppApi.Data
{
    public class ApproveAPIDbContext : DbContext
    {
        public ApproveAPIDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Permit> Permits { get; set; }

    }
}
