using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Data;
using PermitApplicationWebApi.Models;
using UserWebApi.Models;

namespace ApproveAppApi.Data
{
    public class ApproveAPIDbContext : DbContext
    {
        public ApproveAPIDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Permit> Permits { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
