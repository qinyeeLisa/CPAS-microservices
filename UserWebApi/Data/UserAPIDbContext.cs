using Microsoft.EntityFrameworkCore;
using UserWebApi.Models;

namespace UserWebApi.Data
{
    public class UserAPIDbContext : DbContext
    {
        public UserAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}
