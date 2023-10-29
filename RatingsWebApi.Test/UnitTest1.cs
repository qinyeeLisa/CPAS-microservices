using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RatingsWebApi.Controllers;
using RatingsWebApi.Models;
using RatingsWebApi.Test.Data;

namespace RatingsWebApi.Test
{
    public class UnitTest1 : IClassFixture<TestRatingsAPIDbContext>
    {
        private readonly TestRatingsAPIDbContext _mockRatingsDbContext;
        private readonly ILogger<RatingsController> _mockLogger;

        public UnitTest1(TestRatingsAPIDbContext mockRatingsDbContext)
        {
            _mockRatingsDbContext = mockRatingsDbContext;
        }

        [Fact]
        public async Task GetRatings_ShouldReturnAllRatings_Positive()
        {
            // Arrange
            var context = _mockRatingsDbContext.dbContext;
            _mockRatingsDbContext.Setup();

            RatingsController ratingController = new(context, _mockLogger);

            // Act
            var result = await ratingController.GetRatings(1);

            //Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async Task GetRatings_ShouldReturnAllRatings_Negative()
        {
            // Arrange
            var context = _mockRatingsDbContext.dbContext;

            RatingsController ratingController = new(context, _mockLogger);

            // Act
            var result = await ratingController.GetRatings(5);

            //Assert
            var notFoundResult = result.Result as NotFoundResult;
            Assert.Equal(404, notFoundResult.StatusCode);

        }

        [Fact]
        public async Task CreatingRatings_AddRating_Positive()
        {
            // Arrange
            var context = _mockRatingsDbContext.dbContext;

            RatingsController ratingController = new(context, _mockLogger);

            // Act
            var result = await ratingController.CreateRatings(1, "desc3", 3, "test3");

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rating added successfully", okObjectResult.Value);
        }

        [Fact]
        public async Task UpdateRatings_ChangeRating_Positive()
        {
            // Arrange
            var context = _mockRatingsDbContext.dbContext;

            var existingRating = new Ratings()
            {
                RatingId = 3,
                UserId = 1,
                Description = "desc3",
                Rating = 5,
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };
            context.Add(existingRating);
            context.SaveChanges();

            RatingsController ratingController = new(context, _mockLogger);

            // Act
            var result = await ratingController.UpdateRatings(3, "desc3", 5, "test3");

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rating Updated successfully", okObjectResult.Value);
        }

        [Fact]
        public async Task DeleteRatings_RemoveRating_Positive()
        {
            // Arrange
            var context = _mockRatingsDbContext.dbContext;
            var existingRating = new Ratings()
            {
                RatingId = 11,
                UserId = 1,
                Description = "desc11",
                Rating = 5,
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };
            context.Add(existingRating);
            context.SaveChanges();

            RatingsController ratingController = new(context, _mockLogger);

            // Act
            var result = await ratingController.DeleteRatings(11);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rating is deleted successfully.", okObjectResult.Value);
        }

        [Fact]
        public async Task DeleteRatings_RemoveRating_Negative()
        {
            // Arrange
            var context = _mockRatingsDbContext.dbContext;

            RatingsController ratingController = new(context, _mockLogger);

            // Act
            var result = await ratingController.DeleteRatings(1);

            //Assert
            var notFoundObjResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Unable to delete Rating.", notFoundObjResult.Value);
        }
    }
}