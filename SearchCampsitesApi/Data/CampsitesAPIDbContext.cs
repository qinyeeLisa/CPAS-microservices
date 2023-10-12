using Microsoft.EntityFrameworkCore;
using SearchCampsitesApi.Models;

namespace SearchCampsitesApi.Data
{
    public class CampsiteAPIDbContext : DbContext
    {
        public CampsiteAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Campsites> Campsite { get; set; }
       
    }
}
