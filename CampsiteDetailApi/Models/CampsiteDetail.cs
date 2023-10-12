using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace CampsiteDetailApi.Models
{
    [Table("CampsiteDetail", Schema = "dbo")]
    public class CampsiteDetail
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CampsiteDetailId { get; set; }

        [ForeignKey("CampsiteId")]
        public long CampsiteId { get; set; }

        [MaxLength(500)]
        public string AreaName { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime DateTimeCreated { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}
