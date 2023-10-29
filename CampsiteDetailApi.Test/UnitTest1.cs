using CampsiteDetailApi.Controllers;
using CampsiteDetailApi.Models;
using CampsiteDetailApi.Test.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace CampsiteDetailApi.Test
{
    public class UnitTest1 : IClassFixture<TestCampsiteDetailAPIDbContext>
    {
        private readonly TestCampsiteDetailAPIDbContext _mockDbContext;
        private readonly ILogger<CampsiteDetailApiController> _mockLogger;

        public UnitTest1(TestCampsiteDetailAPIDbContext mockDbContext)
        {
            _mockDbContext = mockDbContext;
        }

        [Fact]
        public async Task GetCampsiteDetail_ShouldReturnDetail_Positive()
        {
            // Arrange
            var context = _mockDbContext.campsiteDetailDbContext;
            _mockDbContext.SetupCampsiteDetail();

            var expectedResult = new CampsiteDetail()
            {
                CampsiteDetailId = 87,
                CampsiteId = 1,
                AreaName = "Area1",
                CreatedBy = "test1",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test1",
                DateTimeUpdated = DateTime.Now,
            };

            CampsiteDetailApiController campsiteDetailCtrlr = new(context, _mockLogger);

            // Act
            var result = await campsiteDetailCtrlr.GetCampsiteDetail(87);

            //Assert
            Assert.Equal(expectedResult.AreaName, result.Value.AreaName);
        }

        [Fact]
        public async Task GetCampsiteDetail_ShouldReturnDetail_Negative()
        {
            // Arrange
            var context = _mockDbContext.campsiteDetailDbContext;
            CampsiteDetailApiController campsiteDetailCtrlr = new(context, _mockLogger);

            // Act
            var result = await campsiteDetailCtrlr.GetCampsiteDetail(99);

            //Assert
            var notFoundResult = result.Result as NotFoundResult;
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateCampsiteDetail_ShouldSaveDetail_Positive()
        {
            // Arrange
            var context = _mockDbContext.campsiteDetailDbContext;
            CampsiteDetailApiController campsiteDetailCtrlr = new(context, _mockLogger);

            // Act
            var result = await campsiteDetailCtrlr.CreateCampsiteDetail(1, "Area2", "qy");

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Campsite Detail added successfully", okObjectResult.Value);
        }

        [Fact]
        public async Task DeleteCampsiteDetail_ShouldRemoveDetail_Negative()
        { 
            // Arrange
            var context = _mockDbContext.campsiteDetailDbContext;
            CampsiteDetailApiController campsiteDetailCtrlr = new(context, _mockLogger);

            // Act
            var result = await campsiteDetailCtrlr.DeleteCampsiteDetail(9) as NotFoundObjectResult;

            Assert.Equal(404, result.StatusCode);
        }

    }
}