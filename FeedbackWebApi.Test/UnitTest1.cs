using FeedbackWebApi.Controllers;
using FeedbackWebApi.Models;
using FeedbackWebApi.Models.Dto;
using FeedbackWebApi.Services;
using FeedbackWebApi.Test.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserWebApi.Controllers;
using UserWebApi.Models;

namespace FeedbackWebApi.Test
{
    public class UnitTest1 : IClassFixture<TestFeedbackAPIDbContext>
    {
        private readonly TestFeedbackAPIDbContext _mockDbContext;

        public UnitTest1(TestFeedbackAPIDbContext mockDbContext)
        {
            _mockDbContext = mockDbContext;
        }

        [Fact]
        public async Task AddFeedback_ShouldSaveFeedback_Positive()
        {
            //Arrange
            var feedbackServiceMock = new Mock<FeedbackService>();
            var feedbackContext = _mockDbContext.feedbackDbContext;
            _mockDbContext.SetupUser();

            FeedbackInfoDto newFeedback = new FeedbackInfoDto()
            {
                FeedbackId = 28,
                UserId = 1,
                Title = "feedback1",
                Description = "content"
            };

            FeedbackController feedbackController = new FeedbackController(feedbackContext, feedbackServiceMock.Object);

            // Act
            var result = await feedbackController.AddFeedback(newFeedback);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetFeedback_ShouldReturnFeedback_Positive()
        {
            //Arrange
            var feedbackServiceMock = new Mock<FeedbackService>();
            var feedbackContext = _mockDbContext.feedbackDbContext;
            var testUser = new User()
            {
                UserId = 9,
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

            feedbackContext.Add(testUser);

            var mockFeedback = new Feedback()
            {
                FeedbackId = 13,
                UserId = 9,
                Title = "feedback1",
                Description = "content",
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };
            feedbackContext.Add(mockFeedback);
            feedbackContext.SaveChanges();

            FeedbackController feedbackController = new FeedbackController(feedbackContext, feedbackServiceMock.Object);

            // Act
            var result = await feedbackController.GetFeedback(9);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(okObjectResult.StatusCode, 200);
        }

        [Fact]
        public async Task GetFeedback_ShouldReturnFeedback_Negative()
        {
            //Arrange
            var feedbackServiceMock = new Mock<FeedbackService>();
            var feedbackContext = _mockDbContext.feedbackDbContext;
            var mockFeedback = new Feedback()
            {
                FeedbackId = 24,
                UserId = 9,
                Title = "feedback1",
                Description = "content",
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };
            feedbackContext.Add(mockFeedback);
            feedbackContext.SaveChanges();

            FeedbackController feedbackController = new FeedbackController(feedbackContext, feedbackServiceMock.Object);

            // Act
            var result = await feedbackController.GetFeedback(99);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateFeedback_ShouldUpdateFeedback_Positive()
        {
            //Arrange
            var feedbackServiceMock = new Mock<FeedbackService>();
            var feedbackContext = _mockDbContext.feedbackDbContext;
            _mockDbContext.SetupFeedback();
            var testUser = new User()
            {
                UserId = 66,
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

            feedbackContext.Add(testUser);
            feedbackContext.SaveChanges();

            FeedbackInfoDto feedback = new FeedbackInfoDto()
            {
                FeedbackId = 1,
                UserId = 66,
                Title = "feedback99",
                Description = "content99",
            };

            FeedbackController feedbackController = new FeedbackController(feedbackContext, feedbackServiceMock.Object);

            // Act
            var result = await feedbackController.UpdateFeedback(feedback);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(okObjectResult.StatusCode, 200);
        }

        [Fact]
        public async Task DeleteFeedback_ShouldDeleteUser_Positive()
        {
            // Arrange
            var feedbackServiceMock = new Mock<FeedbackService>();
            var feedbackContext = _mockDbContext.feedbackDbContext;
            var mockFeedback = new Feedback()
            {
                FeedbackId = 24,
                UserId = 9,
                Title = "feedback1",
                Description = "content",
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };
            feedbackContext.Add(mockFeedback);
            feedbackContext.SaveChanges();

            FeedbackController feedbackController = new FeedbackController(feedbackContext, feedbackServiceMock.Object);

            // Act
            var result = await feedbackController.DeleteFeedback(24);

            //Assert
            Assert.IsType<OkResult>(result);
        }
    }
}