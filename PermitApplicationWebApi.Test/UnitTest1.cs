using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using PermitApplicationWebApi.Controllers;
using PermitApplicationWebApi.Models;
using PermitApplicationWebApi.Models.Dto;
using PermitApplicationWebApi.Services;
using PermitApplicationWebApi.Test.Data;
using UserWebApi.Controllers;
using UserWebApi.Models;

namespace PermitApplicationWebApi.Test
{
    public class UnitTest1 : IClassFixture<TestPermitAPIDbContext>
    {
        private readonly TestPermitAPIDbContext _mockPermitDbContext;

        public UnitTest1(TestPermitAPIDbContext mockPermitDbContext)
        {
            _mockPermitDbContext = mockPermitDbContext;
        }

        [Fact]
        public async Task GetPermits_ShouldReturnAllPermits_Positive()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockPermitDbContext.dbContext;
            _mockPermitDbContext.SetupPermit();

            PermitController permitController = new(context, permitService.Object);

            // Act
            var result = await permitController.GetPermits();

            //Assert
            Assert.True(result.Value.Count() > 1);
            
        }

        [Fact]
        public async Task EditPermit_ShouldChangeLocation_Positive()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockPermitDbContext.dbContext;
            var existingPermit = new Permit()
            {
                PermitId = 3,
                UserId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "location3",
                Area = "Area3",
                Status = "Pending",
                CreatedBy = "test3",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test3",
                DateTimeUpdated = DateTime.Now,
            };

            context.Add(existingPermit);
            context.SaveChanges();

            _mockPermitDbContext.SetupUser();

            var permitInfo = new PermitInfoDto()
            {
                Id = 3,
                UserId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "location4567",
                Area = "Area3",
            };

            PermitController permitController = new(context, permitService.Object);

            // Act
            var result = await permitController.EditPermit(permitInfo);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var permit = Assert.IsType<Permit>(okObjectResult.Value);
            Assert.Equal("location4567", permit.Location);
        }

        [Fact]
        public async Task EditPermit_ShouldChangeLocation_Negative()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockPermitDbContext.dbContext;

            var existingUser = new User()
            {
                UserId = 65,
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

            context.Add(existingUser);
            context.SaveChanges();

            var permitInfo = new PermitInfoDto()
            {
                Id = 12,
                UserId = 999,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "location4567",
                Area = "Area3",
            };

            PermitController permitController = new(context, permitService.Object);

            // Act
            var result = await permitController.EditPermit(permitInfo);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreatePermit_ShouldAddPermit_Positive()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockPermitDbContext.dbContext;

            var user = new User()
            {
                UserId = 10,
                Username = "test10",
                Name = "test10",
                Email = "test10@test.com",
                Password = "678",
                Role = 0,
                CreatedBy = "test10",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test10",
                DateTimeUpdated = DateTime.Now,
            };

            context.Add(user);
            context.SaveChanges();

            var newPermitInfo = new PermitInfoDto()
            {
                Id = 13,
                UserId = 10,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "location13",
                Area = "Area13",
            };

            PermitController permitController = new(context, permitService.Object);

            // Act
            var result = await permitController.CreatePermit(newPermitInfo);

            //Assert
            var okObjectResult = result.Result as OkObjectResult;
            Assert.Equal("Permit added successfully", okObjectResult.Value);
        }

        [Fact]
        public async Task CreatePermit_ShouldAddPermit_Negative()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockPermitDbContext.dbContext;

            var newPermitInfo = new PermitInfoDto()
            {
                Id = 15,
                UserId = 19,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "location15",
                Area = "Area15",
            };

            PermitController permitController = new(context, permitService.Object);

            // Act
            var result = await permitController.CreatePermit(newPermitInfo);

            //Assert
            var notFoundResult = result.Result as NotFoundResult;
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task DeletePermit_ShouldRemovePermit_Positive()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockPermitDbContext.dbContext;
            var existingPermit = new Permit()
            {
                PermitId = 22,
                UserId = 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = "location22",
                Area = "Area22",
                Status = "Pending",
                CreatedBy = "test3",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test3",
                DateTimeUpdated = DateTime.Now,
            };

            context.Add(existingPermit);
            context.SaveChanges();

            PermitController permitController = new(context, permitService.Object);

            // Act
            var result = await permitController.DeletePermit(22);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(okObjectResult.StatusCode, 200);
        }
    }
}