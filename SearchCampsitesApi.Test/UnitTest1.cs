using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchCampsitesApi.Controllers;
using SearchCampsitesApi.Test.Data;

namespace SearchCampsitesApi.Test
{
    public class UnitTest1 : IClassFixture<TestCampsitesAPIDbContext>
    {
        private readonly TestCampsitesAPIDbContext _mockCampsitesDbContext;
        private readonly ILogger<SearchCampsitesController> _mockLogger;

        public UnitTest1(TestCampsitesAPIDbContext mockCampsitesDbContext)
        {
            _mockCampsitesDbContext = mockCampsitesDbContext;
        }

        [Fact]
        public async Task CreateCampsites_ShouldAddCampsite_Positive()
        {
            // Arrange
            var context = _mockCampsitesDbContext.dbContext;

            SearchCampsitesController campsiteController = new(context, _mockLogger);

            // Act
            var result = await campsiteController.CreateCampsites(1, "address3", "campsite3", 50, "", "test1");

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Campsite added successfully", okObjectResult.Value);
        }

        [Fact]
        public async Task DeleteCampsites_ShouldRemoveCampsite_Positive()
        {
            // Arrange
            var context = _mockCampsitesDbContext.dbContext;
            _mockCampsitesDbContext.Setup();

            SearchCampsitesController campsiteController = new(context, _mockLogger);

            // Act
            var result = await campsiteController.DeleteCampsite(1);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Campsite is deleted successfully.", okObjectResult.Value);
        }

        [Fact]
        public async Task DeleteCampsites_ShouldRemoveCampsite_Negative()
        {
            // Arrange
            var context = _mockCampsitesDbContext.dbContext;

            SearchCampsitesController campsiteController = new(context, _mockLogger);

            // Act
            var result = await campsiteController.DeleteCampsite(3);

            //Assert
            var notFoundObjResult = Assert.IsType<NotFoundObjectResult>(result); ;
            Assert.Equal(404, notFoundObjResult.StatusCode);
        }
    }
}