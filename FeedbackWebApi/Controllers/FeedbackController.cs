using FeedbackWebApi.Data;
using FeedbackWebApi.Models;
using FeedbackWebApi.Models.Dto;
using FeedbackWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserWebApi.Models;

namespace FeedbackWebApi.Controllers
{
    [ApiController]
    [Route("feedbackapi/feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackAPIDbContext _feedbackAPIDbContext;
        private readonly FeedbackService _feedbackService;

        public FeedbackController(FeedbackAPIDbContext feedbackAPIDbContext, FeedbackService feedbackService)
        {
            _feedbackAPIDbContext = feedbackAPIDbContext;
            _feedbackService = feedbackService;
        }

        [HttpPost("AddFeedback")]
        public async Task<IActionResult> AddFeedback([FromBody] FeedbackInfoDto feedbackInfo)
        {
            var currentUser = _feedbackAPIDbContext.User.Where(u => u.UserId == feedbackInfo.UserId).FirstOrDefault();
            var newFeedback = _feedbackService.MapFeedbackInfoDtoToEntity(feedbackInfo);
            if (currentUser != null)
            {
                newFeedback.CreatedBy = currentUser.Username;
                newFeedback.DateTimeCreated = DateTime.Now;
                newFeedback.UpdatedBy = currentUser.Username;
                newFeedback.DateTimeUpdated = DateTime.Now;
            }
            await _feedbackAPIDbContext.Feedback.AddAsync(newFeedback);
            await _feedbackAPIDbContext.SaveChangesAsync();
            return Ok();
        }

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Feedback>> GetFeedback(long id)
        //{
        //    if (_feedbackAPIDbContext.Feedback == null)
        //    {
        //        return NotFound();
        //    }
        //    var permit = await _feedbackAPIDbContext.Feedback.FindAsync(id);

        //    if (permit == null)
        //    {
        //        return NotFound();
        //    }

        //    return permit;
        //}

        [HttpGet("GetFeedback/{userId}")]
        public async Task<IActionResult> GetFeedback(long userId)
        {
            var existingFeedback = await _feedbackAPIDbContext.Feedback.Where(u => u.UserId == userId).ToListAsync();
            if (existingFeedback.Any())
            {
                return Ok(existingFeedback);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("UpdateFeedback")]
        public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackInfoDto feedbackInfo)
        {
            var currentFeedback = await _feedbackAPIDbContext.Feedback.Where(u => u.FeedbackId == feedbackInfo.FeedbackId).FirstOrDefaultAsync();

            var currentUser = _feedbackAPIDbContext.User.Where(u => u.UserId == feedbackInfo.UserId).FirstOrDefault();

            if (currentFeedback != null && currentUser != null)
            {
                var updatedFeedback = _feedbackService.MapFeedbackInfoDtoToEntity(feedbackInfo);
                updatedFeedback.CreatedBy = currentUser!.Username;
                updatedFeedback.DateTimeCreated = currentFeedback.DateTimeCreated;
                updatedFeedback.UpdatedBy = currentUser!.Username;
                updatedFeedback.DateTimeUpdated = DateTime.Now;
                _feedbackAPIDbContext.Entry(currentFeedback).State = EntityState.Detached;
                _feedbackAPIDbContext.Entry(updatedFeedback).State = EntityState.Modified;
                await _feedbackAPIDbContext.SaveChangesAsync();
                return Ok(updatedFeedback);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("DeleteFeedback/{feedbackId}")]
        public async Task<IActionResult> DeleteFeedback(long feedbackId)
        {
            var currentFeedback = await _feedbackAPIDbContext.Feedback.Where(u => u.FeedbackId == feedbackId).FirstOrDefaultAsync();
            if (currentFeedback != null)
            {
                _feedbackAPIDbContext.Feedback.Remove(currentFeedback);
                await _feedbackAPIDbContext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}