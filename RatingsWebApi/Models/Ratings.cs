using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RatingsWebApi.Models
{
   
        [Table("Ratings", Schema = "dbo")]
        public class Ratings
        {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long RatingId { get; set; }

            [ForeignKey("UserId ")]
            public long UserId { get; set; }

            [MaxLength(500)]
            public string Description { get; set; }

            public int Rating { get; set; }

            [MaxLength(100)]
            public string CreatedBy { get; set; }

            public DateTime DateTimeCreated { get; set; }

            [MaxLength(100)]
            public string UpdatedBy { get; set; }

            public DateTime DateTimeUpdated { get; set; }
        }
    
}
