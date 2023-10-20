using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RatingsWebApi.Models;

namespace RatingsWebApi.Data
{
    public class RatingsAPIDbContext : DbContext
    {
        public RatingsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Ratings> Rating { get; set; }


        
    }
}
