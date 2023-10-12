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

        public Permit Permit { get; set; }
    }
}
