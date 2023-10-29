using ApproveAppApi.Test.Data;
using Microsoft.AspNetCore.Mvc;
using UserWebApi.Controllers;
using UserWebApi.Models.Dto;
using UserWebApi.Models;
using Moq;
using ApproveAppApi.Services;
using ApproveAppApi.Controllers;
using ApproveAppApi.Models.Dto;
using PermitApplicationWebApi.Models;
using UserWebApi.Models.ViewModel;
using UserWebApi.Services;

namespace ApproveAppApi.Test
{
    public class UnitTest1 : IClassFixture<TestApproveAPIDbContext>
    {
        private readonly TestApproveAPIDbContext _mockApproveDbContext;

        public UnitTest1(TestApproveAPIDbContext mockApproveDbContext)
        {
            _mockApproveDbContext = mockApproveDbContext;
        }

        [Fact]
        public async Task ApproveApplication_ShouldChangeStatusToApproved_Positive()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockApproveDbContext.approveDbContext;
            var permit = new Permit()
            {
                PermitId = 1,
                UserId = 56,
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

            context.Add(permit);

            var testUser = new User()
            {
                UserId = 56,
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

            context.Add(testUser);
            context.SaveChanges();

            PermitInfoDto permitInfo = new PermitInfoDto() { Id = 1, UserId = 56 };
            ApproveAppController apprvController = new(context, permitService.Object);

            // Act
            var result = await apprvController.ApproveApplication(permitInfo);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(okObjectResult.StatusCode, 200);
        }

        [Fact]
        public async Task ApproveApplication_ShouldChangeStatusToApproved_Negative()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockApproveDbContext.approveDbContext;
            var permit = new Permit()
            {
                PermitId = 9,
                UserId = 27,
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

            context.Add(permit);

            var testUser = new User()
            {
                UserId = 27,
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

            context.Add(testUser);
            context.SaveChanges();

            PermitInfoDto permitInfo = new PermitInfoDto() { Id = 9, UserId = 1 };
            ApproveAppController apprvController = new(context, permitService.Object);

            // Act
            var result = await apprvController.ApproveApplication(permitInfo);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetPermit_ShouldReturnPermit_Positive()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockApproveDbContext.approveDbContext;
            var permit = new Permit()
            {
                PermitId = 3,
                UserId = 33,
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

            context.Add(permit);
            context.SaveChanges();

            var expectedResult = new Permit()
            {
                PermitId = 3,
                UserId = 33,
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

            ApproveAppController apprvController = new(context, permitService.Object);

            // Act
            var result = await apprvController.GetPermit(3);

            //Assert
            Assert.Equal(expectedResult.Location, result.Value.Location);
        }

        [Fact]
        public async Task GetPermit_ShouldReturnPermit_Negative()
        {
            // Arrange
            var permitService = new Mock<PermitService>();
            var context = _mockApproveDbContext.approveDbContext;

            ApproveAppController apprvController = new(context, permitService.Object);

            // Act
            var result = await apprvController.GetPermit(3);

            //Assert
            var notFoundObjectResult = result.Result as NotFoundObjectResult;
            Assert.Equal("Unable to find the permit", notFoundObjectResult.Value);
        }
    }
}