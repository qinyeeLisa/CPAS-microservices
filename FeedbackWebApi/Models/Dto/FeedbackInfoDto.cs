namespace FeedbackWebApi.Models.Dto
{
    public class FeedbackInfoDto
    {
        public long FeedbackId { get; set; }

        public long UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
