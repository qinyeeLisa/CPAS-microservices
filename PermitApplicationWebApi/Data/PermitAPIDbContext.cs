using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Models;
using System.Collections.Generic;

namespace PermitApplicationWebApi.Data
{
    public class PermitAPIDbContext : DbContext
    {

        public PermitAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Permit> Permits { get; set; }

        
    }
}
