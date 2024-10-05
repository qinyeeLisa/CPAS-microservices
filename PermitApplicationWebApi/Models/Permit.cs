using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermitApplicationWebApi.Models
{
    [Table("Permit", Schema = "dbo")]
    public class Permit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PermitId { get; set; }

        public long UserId { get; set; } 

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string Area { get; set; }

        public string Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }

  
  

}
