using Microsoft.EntityFrameworkCore;
using CampsiteDetailApi.Models;
namespace CampsiteDetailApi.Data
{
    public class CampsiteDetailAPIDbContext : DbContext
    {
        public CampsiteDetailAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CampsiteDetail> CampsiteDetail { get; set; }


    }
}
