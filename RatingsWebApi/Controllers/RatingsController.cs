using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatingsWebApi.Data;
using RatingsWebApi.Models;
using RatingsWebApi.Models.Dto;
using RatingsWebApi.Services;

namespace RatingsWebApi.Controllers
{
    [ApiController]
    [Route("ratingsapi/rating")]
    public class RatingsController : ControllerBase
    {
        private readonly RatingsAPIDbContext _ratingsAPIDbContext;
        private readonly RatingService _ratingService;

        public RatingsController(RatingsAPIDbContext RatingsAPIDbContext, RatingService ratingService)
        {
            _ratingsAPIDbContext = RatingsAPIDbContext;
            _ratingService = ratingService;
        }

        //ok
        [HttpPost("AddRating")]
        public async Task<IActionResult> AddRating([FromBody] RatingInfoDto ratingInfo)
        {
            var currentUser = _ratingsAPIDbContext.User.Where(u => u.UserId == ratingInfo.UserId).FirstOrDefault();
            var newRating = _ratingService.MapRatingInfoDtoToEntity(ratingInfo);
            if (currentUser != null)
            {
                newRating.CreatedBy = currentUser.Username;
                newRating.DateTimeCreated = DateTime.Now;
                newRating.UpdatedBy = currentUser.Username;
                newRating.DateTimeUpdated = DateTime.Now;
            }

            await _ratingsAPIDbContext.Rating.AddAsync(newRating);
            await _ratingsAPIDbContext.SaveChangesAsync();
            return Ok("Rating added successfully");
        }

        //ok
        [HttpGet("GetRating/{userId}")]
        public async Task<IActionResult> GetRating(long userId)
        {
            //returns whole object
            var existingRating = await _ratingsAPIDbContext.Rating.Where(u => u.UserId == userId).ToListAsync();
            if (existingRating.Any())
            {
                return Ok(existingRating);
            }

            else
            {
                return NotFound();
            }
        }

        
        //ok
        [HttpPut("UpdateRating")]
        public async Task<IActionResult> UpdateRating([FromBody] RatingInfoDto ratingInfo)
        {
            var currentRating = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId == ratingInfo.RatingId).FirstOrDefaultAsync();

            var currentUser = _ratingsAPIDbContext.User.Where(u => u.UserId == ratingInfo.UserId).FirstOrDefault();

            if (currentRating != null && currentUser != null)
            {
                var updatedRating = _ratingService.MapRatingInfoDtoToEntity(ratingInfo);
                updatedRating.CreatedBy = currentUser!.Username;
                updatedRating.DateTimeCreated = currentRating.DateTimeCreated;
                updatedRating.UpdatedBy = currentUser!.Username;
                updatedRating.DateTimeUpdated = DateTime.Now;
                _ratingsAPIDbContext.Entry(currentRating).State = EntityState.Detached;
                _ratingsAPIDbContext.Entry(updatedRating).State = EntityState.Modified;
                await _ratingsAPIDbContext.SaveChangesAsync();
                return Ok(updatedRating);
            }
            else
            {
                return NotFound();
            }
        }

        //ok
        [HttpDelete("DeleteRating/{ratingId}")]
        public async Task<IActionResult> DeleteRatings(int ratingId)
        {
            var ratingDelete = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId == ratingId).FirstOrDefaultAsync();
            if (ratingDelete != null)
            {
                _ratingsAPIDbContext.Rating.Remove(ratingDelete);
                await _ratingsAPIDbContext.SaveChangesAsync();
                return Ok("Rating is deleted successfully.");
            }
            else
            {
                return NotFound("Unable to delete Rating.");
            }
        }
    }
}
