using CampsiteDetailApi.Data;
using CampsiteDetailApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampsiteDetailApi.Test.Data
{
    public class TestCampsiteDetailAPIDbContext : IDisposable
    {
        public CampsiteDetailAPIDbContext campsiteDetailDbContext { get; private set; }

        public TestCampsiteDetailAPIDbContext()
        {
            var options = new DbContextOptionsBuilder<CampsiteDetailAPIDbContext>()
            .UseInMemoryDatabase(databaseName: "CampsiteDetailDbTest")
            .Options;

            campsiteDetailDbContext = new CampsiteDetailAPIDbContext(options);
        }

        public void SetupCampsiteDetail()
        {
            var campsiteDetail = new CampsiteDetail()
            {
                CampsiteDetailId = 87,
                CampsiteId = 1,
                AreaName = "Area1",
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };

            campsiteDetailDbContext.Add(campsiteDetail);
            campsiteDetailDbContext.SaveChanges();
        }

        public void Dispose()
        {
            campsiteDetailDbContext.Dispose();
        }
    }
}
