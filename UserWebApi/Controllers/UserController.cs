using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using UserWebApi.Data;
using UserWebApi.Models;
using UserWebApi.Models.Dto;
using UserWebApi.Models.ViewModel;
using UserWebApi.Services;

namespace UserWebApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserAPIDbContext _userAPIDbContext;
        private readonly UserService _userService;

        public UserController(UserAPIDbContext userAPIDbContext, UserService userService)
        {
            _userAPIDbContext = userAPIDbContext;
            _userService = userService;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userAPIDbContext.User.Where(u => u.Email == loginDto.Email && u.Password == loginDto.Password).FirstOrDefaultAsync();

            if (user != null)
            {
                return Ok("Login Successful.");
            }
            else
            {
                return NotFound("User does not exist.");
            }
        }
        
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] UserInfoDto userInfo)
        {
            var isUsernameExist = await _userAPIDbContext.User.Where(u => u.Username == userInfo.Username).FirstOrDefaultAsync();

            if (isUsernameExist != null)
            {
                var isEmailExist = await _userAPIDbContext.User.Where(u => u.Email == userInfo.Email).FirstOrDefaultAsync();
                if (isEmailExist == null)
                {
                    var newUser = _userService.MapUserInfoDtoToEntity(userInfo);
                    newUser.CreatedBy = userInfo.Username;
                    newUser.DateTimeCreated = DateTime.Now;
                    newUser.UpdatedBy = userInfo.Username;
                    newUser.DateTimeUpdated = DateTime.Now;
                    await _userAPIDbContext.User.AddAsync(newUser);
                    await _userAPIDbContext.SaveChangesAsync();
                    return Ok("User is created successfully");
                }
                else
                {
                    return Conflict("Email exist. Please login using your email.");
                }
            }
            else
            {
                return Conflict("Username exist. Please choose another username.");
            }
            
        }

        [HttpPost("GetUserProfile")]
        [ProducesResponseType(typeof(UserProfileViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserProfile(long userId)
        {
            var user = await _userAPIDbContext.User.Where(u => u.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                var userProfile = _userService.MapEntityToUserProfileViewModel(user);
                return Ok(userProfile);
            }
            else
            {
                return NotFound("Unable to get user profile.");
            }
        }

        [HttpPost("UpdateUser")]
        //[ProducesResponseType(typeof(UserProfileViewModel), StatusCodes.Status200OK)]
        //not sure after update need to return user info or not
        public async Task<IActionResult> UpdateUser([FromBody] UserInfoDto userInfo)
        {
            var currentUser = await _userAPIDbContext.User.Where(u => u.UserId == userInfo.Id).FirstOrDefaultAsync();
            if (currentUser != null)
            {
                var updatedUser = _userService.MapUserInfoDtoToEntity(userInfo);
                updatedUser.Email = currentUser.Email;
                updatedUser.Password = currentUser.Password;
                updatedUser.CreatedBy = currentUser.Username;
                updatedUser.DateTimeCreated = currentUser.DateTimeCreated;
                updatedUser.UpdatedBy = userInfo.Username;
                updatedUser.DateTimeUpdated = DateTime.Now;
                _userAPIDbContext.Entry(currentUser).State = EntityState.Detached;
                _userAPIDbContext.Entry(updatedUser).State = EntityState.Modified;
                await _userAPIDbContext.SaveChangesAsync();
                return Ok("User is updated successfully");             
            }
            else
            {
                return NotFound("Unable to update user.");
            }
        }

        [HttpDelete]
        //[ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            var currentUser = await _userAPIDbContext.User.Where(u => u.UserId == userId).FirstOrDefaultAsync();
            if (currentUser != null)
            {
                _userAPIDbContext.User.Remove(currentUser);
                await _userAPIDbContext.SaveChangesAsync();
                return Ok("User is deleted successfully.");
            }
            else
            {
                return NotFound("Unable to delete user.");
            }          
        }        
    }
}
