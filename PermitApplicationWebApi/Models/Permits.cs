using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermitApplicationWebApi.Models
{
    [Table("permits", Schema = "dbo")]
    public class Permits
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("userid")]
        public int UserId { get; set; }

        [Column("startdate")]
        public DateTime StartDate { get; set; }

        [Column("enddate")]
        public DateTime EndDate { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("area")]
        public string Area { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("createdby")]
        public string CreatedBy { get; set; }

        [Column("datetimecreated")]
        public DateTime DateTimeCreated { get; set; }

        [Column("updatedby")]
        public string UpdatedBy { get; set; }

        [Column("datetimeupdated")]
        public DateTime DateTimeUpdated { get; set; }
    }
}
