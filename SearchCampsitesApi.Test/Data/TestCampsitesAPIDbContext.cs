using Microsoft.EntityFrameworkCore;
using SearchCampsitesApi.Data;
using SearchCampsitesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCampsitesApi.Test.Data
{
    public class TestCampsitesAPIDbContext : IDisposable
    {
        public CampsiteAPIDbContext dbContext { get; private set; }

        public TestCampsitesAPIDbContext()
        {
            var options = new DbContextOptionsBuilder<CampsiteAPIDbContext>()
            .UseInMemoryDatabase(databaseName: "CampsitesDbTest")
            .Options;

            dbContext = new CampsiteAPIDbContext(options);
        }

        public void Setup()
        {
            List<Campsites> campsites = new List<Campsites>()
            {
                new Campsites()
                {
                    CampsiteId = 1,
                    UserId = 1,
                    Address = "address1",
                    CampsiteName = "campsite1",
                    Size = 50,
                    remarks = "",
                    CreatedBy = "test1",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test1",
                    DateTimeUpdated = DateTime.Now,
                },
                new Campsites()
                {
                    CampsiteId = 2,
                    UserId = 1,
                    Address = "address2",
                    CampsiteName = "campsite2",
                    Size = 50,
                    remarks = "",
                    CreatedBy = "test1",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test1",
                    DateTimeUpdated = DateTime.Now,
                }
            };

            dbContext.AddRange(campsites);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
