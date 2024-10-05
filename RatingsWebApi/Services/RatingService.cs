using RatingsWebApi.Models;
using RatingsWebApi.Models.Dto;

namespace RatingsWebApi.Services
{
    public class RatingService
    {
        public Ratings MapRatingInfoDtoToEntity(RatingInfoDto ratingInfo)
        {
            return new Ratings
            {
                RatingId = ratingInfo.RatingId,
                UserId = ratingInfo.UserId,
                Description = ratingInfo.Description,
                Rating = ratingInfo.Rating,
                CreatedBy = ratingInfo.CreatedBy,
                UpdatedBy = ratingInfo.UpdatedBy

            };
        }
    }
}
