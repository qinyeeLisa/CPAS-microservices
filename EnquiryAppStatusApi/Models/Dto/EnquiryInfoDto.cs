namespace EnquiryAppStatusApi.Models.Dto
{
    public class EnquiryInfoDto
    {
        public long EnquiryId { get; set; }

        public long PermitId { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}
