using PermitApplicationWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnquiryAppStatusApi.Models.ViewModel
{
    public class EnquiryProfileViewModel
    {

        public long Id { get; set; }

        public long PermitId { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }


    }
}
