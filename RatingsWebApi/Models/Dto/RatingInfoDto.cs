namespace RatingsWebApi.Models.Dto
{
    public class RatingInfoDto
    {
        public long RatingId { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
        public int Rating {  get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
