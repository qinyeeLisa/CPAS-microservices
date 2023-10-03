using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserWebApi.Models
{
    [Table("User", Schema = "dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(320)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        public int Role { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTime DateTimeCreated { get; set; }

        [MaxLength(100)]
        public string UpdatedBy { get; set; }

        public DateTime DateTimeUpdated { get; set; }
    }
}
