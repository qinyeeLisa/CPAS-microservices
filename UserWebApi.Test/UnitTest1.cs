using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using UserWebApi.Controllers;
using UserWebApi.Data;
using UserWebApi.Models;
using UserWebApi.Models.Dto;
using UserWebApi.Models.ViewModel;
using UserWebApi.Services;
using UserWebApi.Test.Data;

namespace UserWebApi.Test
{
    public class UnitTest1 : IClassFixture<TestUserAPIDbContext>
    {
        private readonly TestUserAPIDbContext _mockDbContext;
        private readonly UserService _mockUserService;
        private readonly EmailSender _mockEmailSender;

        public UnitTest1(TestUserAPIDbContext mockDbContext)
        {
            _mockDbContext = mockDbContext;
        }

        [Fact]
        public async Task Login_ShouldReturnUserProfile_Positive()
        {
            // Arrange
            var context = _mockDbContext.dbContext;
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

            var expectedResult = new User()
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

            LoginDto loginDto = new LoginDto() { Email = "test1@test.com", Password = "678" };
            UserController userController = new(context, _mockUserService, _mockEmailSender);

            // Act
            var result = await userController.Login(loginDto);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<User>(okObjectResult.Value);
            Assert.Equal(expectedResult.Username, user.Username);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public async Task Login_ShouldReturnUserProfile_Negative()
        {
            // Arrange
            var context = _mockDbContext.dbContext;
            LoginDto loginDto = new LoginDto() { Email = "fail@test.com", Password = "345" };
            UserController userController = new(context, _mockUserService, _mockEmailSender);

            // Act
            var result = await userController.Login(loginDto);

            //Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("User does not exist.", notFoundObjectResult.Value);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public async Task Register_AddUserSuccessful_Positive()
        {
            // Arrange
            var userServiceMock = new Mock<UserService>();
            var emailSenderMock = new Mock<IEmailSender>();
            var context = _mockDbContext.dbContext;

            var expectedResult = new User()
            {
                UserId = 6,
                Username = "test6",
                Name = "test6",
                Email = "test6@test.com",
                Password = "678",
                Role = 0,
                CreatedBy = "test6",
                DateTimeCreated = DateTime.Now,
                UpdatedBy = "test6",
                DateTimeUpdated = DateTime.Now,
            };

            UserInfoDto userInfo = new UserInfoDto()
            {
                Username = "test6",
                Name = "test6",
                Email = "test6@test.com",
                Password = "678",
                Role = 0
            };
            UserController userController = new(context, userServiceMock.Object, emailSenderMock.Object);

            // Act
            var result = await userController.Register(userInfo);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<User>(okObjectResult.Value);
            Assert.Equal(expectedResult.Username, user.Username);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public async Task Register_AddUserSuccessful_Negative()
        {
            // Arrange
            var context = _mockDbContext.dbContext;
            UserInfoDto userInfo = new UserInfoDto()
            {
                Username = "test1",
                Name = "test1",
                Email = "test1@test.com",
                Password = "678",
                Role = 0
            };
            UserController userController = new(context, _mockUserService, _mockEmailSender);

            // Act
            var result = await userController.Register(userInfo);

            //Assert
            var objResult = Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal("Username exist. Please choose another username.", objResult.Value);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnUserProfile_Positive()
        {
            // Arrange
            var userServiceMock = new Mock<UserService>();
            var context = _mockDbContext.dbContext;
            var testUser = new User()
            {
                UserId = 13,
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

            UserController userController = new(context, userServiceMock.Object, _mockEmailSender);

            // Act
            var result = await userController.GetUserProfile(13);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            var user = (UserProfileViewModel)okObjectResult.Value;
            var expectedUsername = context.User.Where(x => x.UserId == 13).Select(u => u.Username).FirstOrDefault();

            Assert.Equal(expectedUsername, user.Username);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public async Task GetUserProfile_ShouldReturnUserProfile_Negative()
        {
            // Arrange
            var context = _mockDbContext.dbContext;
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

            context.Add(testUser);
            context.SaveChanges();

            UserController userController = new(context, _mockUserService, _mockEmailSender);

            // Act
            var result = await userController.GetUserProfile(2);

            //Assert
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Unable to get user profile.", notFoundObjectResult.Value);
            context.ChangeTracker.Clear();

        }

        [Fact]
        public async Task UpdateUser_ShouldChangeEmail_Positive()
        {
            // Arrange
            var userServiceMock = new Mock<UserService>();
            var context = _mockDbContext.dbContext;
            _mockDbContext.Setup();

            UserInfoDto userInfo = new UserInfoDto()
            {
                Id = 1,
                Username = "test999",
                Name = "test1",
                Email = "test1@test.com",
                Password = "678",
                Role = 0
            };
            UserController userController = new(context, userServiceMock.Object, _mockEmailSender);

            // Act
            var result = await userController.UpdateUser(userInfo);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(okObjectResult.StatusCode, 200);
            context.ChangeTracker.Clear();
        }

        [Fact]
        public async Task DeleteUser_ShouldRemoveUser_Positive()
        {
            // Arrange
            var context = _mockDbContext.dbContext;
            //_mockDbContext.Setup();

            UserController userController = new(context, _mockUserService, _mockEmailSender);

            // Act
            var result = await userController.DeleteUser(1);

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(okObjectResult.StatusCode, 200);
            context.ChangeTracker.Clear();
        }
    }
}