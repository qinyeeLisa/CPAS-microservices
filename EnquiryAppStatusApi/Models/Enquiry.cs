using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PermitApplicationWebApi.Models;

namespace EnquiryAppStatusApi.Models
{

    public class Enquiry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long EnquiryId { get; set; }

        [ForeignKey("PermitId")]
        public long PermitId { get; set; }

        public string Status { get; set; }

        public string CreatedBy {  get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }

        internal Permit Permit { get; set; }

       
    }
}
