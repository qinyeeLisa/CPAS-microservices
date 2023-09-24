using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using UserWebApi.Data;
using UserWebApi.Models;

namespace UserWebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserAPIDbContext _userAPIDbContext;

        public UserController(UserAPIDbContext userAPIDbContext)
        {
            _userAPIDbContext = userAPIDbContext;
        }

        [HttpGet("login")]
        //[ProducesResponseType(typeof(ErrorViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _userAPIDbContext.Users.Where(u => u.Email == loginDto.Email && u.Password == loginDto.Password).FirstOrDefaultAsync();

            if (result != null)
            {
                return Ok("Login successful.");
            }
            else { return BadRequest(); }
        }

        [Route("[action]")]
        [HttpPost]
        //[ProducesResponseType(typeof(ErrorViewModel),200)]
        //[ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser(Users user)
        {
            await _userAPIDbContext.Users.AddAsync(user);
            await _userAPIDbContext.SaveChangesAsync();
            return Ok();
        }

        [Route("[action]")]
        [HttpPut]
        public async Task<IActionResult> UpdateUser(Users user)
        {
            _userAPIDbContext.Users.Update(user);
            await _userAPIDbContext.SaveChangesAsync();
            return Ok();
        }

        [Route("[action]")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _userAPIDbContext.Users.FindAsync(userId);
            _userAPIDbContext.Users.Remove(user);
            await _userAPIDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
