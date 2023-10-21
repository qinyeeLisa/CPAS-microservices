using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatingsWebApi.Data;
using RatingsWebApi.Models;

namespace RatingsWebApi.Controllers
{
    [Route("ratingsapi/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly RatingsAPIDbContext _ratingsAPIDbContext;
        private readonly ILogger<RatingsController> _logger;

        public RatingsController(RatingsAPIDbContext RatingsAPIDbContext, ILogger<RatingsController> logger)
        {
            _ratingsAPIDbContext = RatingsAPIDbContext;
            _logger = logger;
        }



        [HttpGet]
        [Route("Ratings")]
        public async Task<ActionResult<Ratings>> GetRatings(int RatingId)
        {
            var rating = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId== RatingId).FirstOrDefaultAsync();
            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }
        /*returns the size list in the case frontEnd wants to get the list of sizes available as a dropdown*/


        [HttpPost]
        [Route("CreateRatings")]
        public async Task<IActionResult> CreateRatings(int userid, String Description, int rating, String userName)
        {
            Ratings createRating = new Ratings
            {
                UserId= userid,
                Description= Description,
                CreatedBy = userName,
                UpdatedBy = userName,
                Rating = rating,
                DateTimeCreated = DateTime.Now,
                DateTimeUpdated = DateTime.Now


            };
            //  List<Campsites> campsitesList=await _ratingsAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            await _ratingsAPIDbContext.Rating.AddAsync(createRating);
            await _ratingsAPIDbContext.SaveChangesAsync();
            return Ok("Rating added successfully");
        }

        [HttpPut]
        [Route("UpdateRatings")]
        public async Task<IActionResult> UpdateRatings(int ratingId, String newDescription, int newRating, String OwnerName)
        {
            var updateRating = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId == ratingId).FirstOrDefaultAsync();

            updateRating.RatingId = updateRating.RatingId;
            updateRating.UserId = updateRating.UserId;
            updateRating.Description = newDescription;
            updateRating.Rating = newRating;
            updateRating.CreatedBy = updateRating.CreatedBy;
            updateRating.UpdatedBy = OwnerName;
            updateRating.DateTimeCreated = updateRating.DateTimeCreated;
            updateRating.DateTimeUpdated = DateTime.Now;
            _ratingsAPIDbContext.Entry(updateRating).State = EntityState.Detached;
            _ratingsAPIDbContext.Entry(updateRating).State = EntityState.Modified;
            await _ratingsAPIDbContext.SaveChangesAsync();
            return Ok("Rating Updated successfully");
        }

        [HttpDelete]
        //[ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> DeleteRatings(int ratingID)
        {
            var ratingDelete = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId == ratingID).FirstOrDefaultAsync();
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
