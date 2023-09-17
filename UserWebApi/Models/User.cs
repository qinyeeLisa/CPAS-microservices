using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserWebApi.Models
{
    [Table("user", Schema = "dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role")]
        public int Role { get; set; }

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
