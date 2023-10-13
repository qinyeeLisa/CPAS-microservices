using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RatingsWebApi.Data;
using RatingsWebApi.Models;

namespace RatingsWebApi.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<Ratings>> GetCampsiteDetail(int campsiteDetailId)
        {
            var rating = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId==campsiteDetailId).FirstOrDefaultAsync();
            if (rating == null)
            {
                return NotFound();
            }

            return rating;
        }
        /*returns the size list in the case frontEnd wants to get the list of sizes available as a dropdown*/


        [HttpPost]
        [Route("CreateRatings")]
        public async Task<IActionResult> CreateCampsiteDetail(int CampsiteId, String AreaName, String OwnerName)
        {
            Ratings rating = new Ratings
            {
                RatingId = CampsiteId,
                Description = AreaName,
                CreatedBy = OwnerName,
                UpdatedBy = OwnerName,
                DateTimeCreated = DateTime.Now,
                DateTimeUpdated = DateTime.Now


            };
            //  List<Campsites> campsitesList=await _ratingsAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            await _ratingsAPIDbContext.Rating.AddAsync(rating);
            await _ratingsAPIDbContext.SaveChangesAsync();
            return Ok("Campsite Detail added successfully");
        }

        [HttpPut]
        [Route("UpdateCampsites")]
        public async Task<IActionResult> UpdateCampsiteDetail(int CampsiteDetailId, String AreaName, String OwnerName)
        {
            var currentCampsiteDetail = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId == CampsiteDetailId).FirstOrDefaultAsync();

            currentCampsiteDetail.RatingId = currentCampsiteDetail.RatingId;
            currentCampsiteDetail.UserId = currentCampsiteDetail.UserId;
            currentCampsiteDetail.Description = AreaName;
            currentCampsiteDetail.CreatedBy = currentCampsiteDetail.CreatedBy;
            currentCampsiteDetail.UpdatedBy = OwnerName;
            currentCampsiteDetail.DateTimeCreated = currentCampsiteDetail.DateTimeCreated;
            currentCampsiteDetail.DateTimeUpdated = DateTime.Now;
            //  List<Campsites> campsitesList=await _ratingsAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            _ratingsAPIDbContext.Entry(currentCampsiteDetail).State = EntityState.Detached;
            _ratingsAPIDbContext.Entry(currentCampsiteDetail).State = EntityState.Modified;
            await _ratingsAPIDbContext.SaveChangesAsync();
            return Ok("Campsite Detail Updated successfully");
        }

        [HttpDelete]
        //[ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> DeleteCampsiteDetail(int campsiteDetailId)
        {
            var campsiteDetail = await _ratingsAPIDbContext.Rating.Where(u => u.RatingId == campsiteDetailId).FirstOrDefaultAsync();
            if (campsiteDetail != null)
            {
                _ratingsAPIDbContext.Rating.Remove(campsiteDetail);
                await _ratingsAPIDbContext.SaveChangesAsync();
                return Ok("Campsite Detail is deleted successfully.");
            }
            else
            {
                return NotFound("Unable to delete campSite.");
            }
        }
    }
}
