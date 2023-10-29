using FeedbackWebApi.Data;
using FeedbackWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserWebApi.Models;

namespace FeedbackWebApi.Test.Data
{
    public class TestFeedbackAPIDbContext : IDisposable
    {
        public FeedbackAPIDbContext feedbackDbContext { get; private set; }

        public TestFeedbackAPIDbContext()
        {
            var options = new DbContextOptionsBuilder<FeedbackAPIDbContext>()
            .UseInMemoryDatabase(databaseName: "FeedbackDbTest")
            .EnableSensitiveDataLogging()
            .Options;

            feedbackDbContext = new FeedbackAPIDbContext(options);
        }

        public void SetupFeedback()
        {
            var Feedback = new Feedback()
            {
                FeedbackId = 1,
                UserId = 1,
                Title = "feedback1",
                Description = "content",
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };

            feedbackDbContext.Add(Feedback);
            feedbackDbContext.SaveChanges();
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

            feedbackDbContext.Add(user);
            feedbackDbContext.SaveChanges();
        }

        public void Dispose()
        {
            feedbackDbContext.Dispose();
        }
    }
}
