using FeedbackWebApi.Data;
using FeedbackWebApi.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FeedbackWebApi.Models;

namespace FeedbackWebApi.Services
{
    public class FeedbackService
    {
        public Feedback MapFeedbackInfoDtoToEntity(FeedbackInfoDto FeedbackInfo) 
        {
            return new Feedback
            {
                FeedbackId = FeedbackInfo.FeedbackId,
                UserId = FeedbackInfo.UserId,
                Title = FeedbackInfo.Title,
                Description = FeedbackInfo.Description,
                CreatedBy = FeedbackInfo.CreatedBy,
                UpdatedBy = FeedbackInfo.UpdatedBy
            };
        }

        //public FeedbackProfileViewModel MapEntityToFeedbackProfileViewModel(Feedback Feedback)
        //{
        //    return new FeedbackProfileViewModel
        //    {
        //        Id = Feedback.FeedbackId,
        //        Name = Feedback.Name,
        //        Email = Feedback.Email,
        //        Role = Feedback.Role,
        //        CreatedBy = Feedback.CreatedBy,
        //        DateTimeCreated = Feedback.DateTimeCreated,
        //        UpdatedBy = Feedback.UpdatedBy,
        //        DateTimeUpdated = Feedback.DateTimeUpdated
        //    };
        //}

    }
}
