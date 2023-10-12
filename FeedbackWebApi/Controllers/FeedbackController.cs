using FeedbackWebApi.Data;
using FeedbackWebApi.Models.Dto;
using FeedbackWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserWebApi.Data;
using UserWebApi.Services;

namespace FeedbackWebApi.Controllers
{
    [ApiController]
    [Route("api/feedback")]
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
            var newFeedback = _feedbackService.MapFeedbackInfoDtoToEntity(feedbackInfo);
            await _feedbackAPIDbContext.Feedback.AddAsync(newFeedback);
            await _feedbackAPIDbContext.SaveChangesAsync();
            return Ok("Feedback is submitted successfully.");
        }

        [HttpPost("GetFeedback")]
        public async Task<IActionResult> GetFeedback(long feedbackId)
        {
            var feedback = await _feedbackAPIDbContext.Feedback.Where(u => u.FeedbackId == feedbackId).FirstOrDefaultAsync();

            if (feedback != null)
            {
                //var feedbackInfo = _userService.MapEntityToUserProfileViewModel(user);
                return Ok(feedback);
            }
            else
            {
                return NotFound("Unable to get feedback info.");
            }
        }


        //[HttpPost("UpdateFeedback")]
        //public async Task<IActionResult> UpdateFeedback([FromBody] FeedbackInfoDto feedbackInfo)
        //{
        //    var currentFeedback = await _feedbackAPIDbContext.Feedback.Where(u => u.FeedbackId == feedbackInfo.FeedbackId).FirstOrDefaultAsync();
        //    if (currentFeedback != null)
        //    {
        //        var updatedFeedback = _feedbackService.MapFeedbackInfoDtoToEntity(userInfo);
        //        updatedFeedback.CreatedBy = currentFeedback.Username;
        //        updatedFeedback.DateTimeCreated = currentFeedback.DateTimeCreated;
        //        updatedFeedback.UpdatedBy = currentFeedback.Username;
        //        updatedFeedback.DateTimeUpdated = DateTime.Now;
        //        _userAPIDbContext.Entry(currentUser).State = EntityState.Detached;
        //        _userAPIDbContext.Entry(updatedFeedback).State = EntityState.Modified;
        //        await _userAPIDbContext.SaveChangesAsync();
        //        return Ok("User is updated successfully");
        //    }
        //    else
        //    {
        //        return NotFound("Unable to update user.");
        //    }
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteFeedback(long feedbackId)
        {
            var currentFeedback = await _feedbackAPIDbContext.Feedback.Where(u => u.FeedbackId == feedbackId).FirstOrDefaultAsync();
            if (currentFeedback != null)
            {
                _feedbackAPIDbContext.Feedback.Remove(currentFeedback);
                await _feedbackAPIDbContext.SaveChangesAsync();
                return Ok("Feedback is deleted successfully.");
            }
            else
            {
                return NotFound("Unable to delete feedback.");
            }
        }
    }
}