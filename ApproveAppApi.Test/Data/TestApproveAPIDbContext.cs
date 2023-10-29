using ApproveAppApi.Data;
using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWebApi.Models;

namespace ApproveAppApi.Test.Data
{
    public class TestApproveAPIDbContext : IDisposable
    {
        public ApproveAPIDbContext approveDbContext { get; private set; }

        public TestApproveAPIDbContext()
        {
            var options = new DbContextOptionsBuilder<ApproveAPIDbContext>()
            .UseInMemoryDatabase(databaseName: "ApproveDbTest")
            .Options;

            approveDbContext = new ApproveAPIDbContext(options);
        }

        public void SetupPermit()
        {
            var permit = new Permit()
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
            };

            approveDbContext.Add(permit);
            approveDbContext.SaveChanges();
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

            approveDbContext.Add(user);
            approveDbContext.SaveChanges();
        }

        public void Dispose()
        {
            approveDbContext.Dispose();
        }
    }
}
