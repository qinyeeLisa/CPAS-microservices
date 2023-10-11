using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserWebApi.Models;

namespace PermitApplicationWebApi.Models
{
    [Table("Permit", Schema = "dbo")]
    public class Permit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PermitId { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; } // Foreign key

        public User User { get; set; } // Navigation property to related User entity

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string Area { get; set; }

        public int Status { get; set; }

        public string CreatedBy { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }


}
