using Microsoft.EntityFrameworkCore;
using RatingsWebApi.Data;
using RatingsWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingsWebApi.Test.Data
{
    public class TestRatingsAPIDbContext : IDisposable
    {
        public RatingsAPIDbContext dbContext { get; private set; }

        public TestRatingsAPIDbContext()
        {
            var options = new DbContextOptionsBuilder<RatingsAPIDbContext>()
            .UseInMemoryDatabase(databaseName: "RatingsDbTest")
            .Options;

            dbContext = new RatingsAPIDbContext(options);
        }

        public void Setup()
        {
            List<Ratings> rating = new List<Ratings>()
            {
                new Ratings()
                {
                    RatingId = 1,
                    UserId = 1,
                    Description = "desc1",
                    Rating = 5,
                    CreatedBy = "test1",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test1",
                    DateTimeUpdated = DateTime.Now,
                },
                new Ratings()
                {
                    RatingId = 2,
                    UserId = 1,
                    Description = "desc2",
                    Rating = 5,
                    CreatedBy = "test1",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test1",
                    DateTimeUpdated = DateTime.Now,
                }
            };

            dbContext.AddRange(rating);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
