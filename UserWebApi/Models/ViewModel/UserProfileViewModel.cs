namespace UserWebApi.Models.ViewModel
{
    public class UserProfileViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int Role { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DateTimeCreated { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? DateTimeUpdated { get; set; }
    }
}
