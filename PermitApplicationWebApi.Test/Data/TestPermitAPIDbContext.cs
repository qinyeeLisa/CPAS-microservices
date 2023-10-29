using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Data;
using PermitApplicationWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWebApi.Models;

namespace PermitApplicationWebApi.Test.Data
{
    public class TestPermitAPIDbContext : IDisposable
    {
        public PermitAPIDbContext dbContext { get; private set; }

        public TestPermitAPIDbContext()
        {
            var options = new DbContextOptionsBuilder<PermitAPIDbContext>()
            .UseInMemoryDatabase(databaseName: "PermitDbTest")
            .Options;

            dbContext = new PermitAPIDbContext(options);
        }

        public void SetupPermit()
        {
            List<Permit> permit = new List<Permit>()
            {
                new Permit()
                {
                    PermitId = 1,
                    UserId = 1,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Location = "location1",
                    Area = "Area1",
                    Status = "Pending",
                    CreatedBy = "test1",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test1",
                    DateTimeUpdated = DateTime.Now,
                },
                new Permit()
                {
                    PermitId = 2,
                    UserId = 2,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                    Location = "location2",
                    Area = "Area2",
                    Status = "Approved",
                    CreatedBy = "test2",
                    DateTimeCreated = DateTime.Now,
                    UpdatedBy = "test2",
                    DateTimeUpdated = DateTime.Now,
                }
            };

            dbContext.AddRange(permit);
            dbContext.SaveChanges();
        }

        public void SetupUser()
        {
            var user = new User()
            {
                UserId = 1,
                Username = "test1",
                Name = "test1",
                Email = "test1@test.com",
                Password = "678",
                Role = 0,
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };

            dbContext.Add(user);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
