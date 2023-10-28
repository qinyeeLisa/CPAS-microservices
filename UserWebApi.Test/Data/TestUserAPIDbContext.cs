using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWebApi.Data;
using UserWebApi.Models;

namespace UserWebApi.Test.Data
{
    public class TestUserAPIDbContext : IDisposable
    {
        public UserAPIDbContext dbContext { get; private set; }

        public TestUserAPIDbContext()
        {
            // Set up your DbContext here, e.g., using an in-memory database.
            var options = new DbContextOptionsBuilder<UserAPIDbContext>()
            .UseInMemoryDatabase(databaseName: "UserDbTest")
            .EnableSensitiveDataLogging()
            .Options;

            dbContext = new UserAPIDbContext(options);
        }

        public void Setup()
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
            // Clean up resources, if necessary.
            dbContext.Dispose();
        }
        //public TestUserAPIDbContext(DbContextOptions<UserAPIDbContext> options) : base(options)
        //{
        //}
    }
}
