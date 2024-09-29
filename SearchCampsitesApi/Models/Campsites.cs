using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using UserWebApi.Models;

namespace SearchCampsitesApi.Models
{
    [Table("Campsites", Schema = "dbo")]
    public class Campsites
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CampsiteId { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }

        internal readonly User User; // Navigation property to related User entity

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(500)]
        public string CampsiteName { get; set; }

        public int Size { get; set; }

        [MaxLength(500)]
        public string Remarks { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime DateTimeCreated { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }


    }
}
