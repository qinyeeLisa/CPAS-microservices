using System.ComponentModel;

namespace PermitApplicationWebApi.Models.Dto
{
    public class PermitInfoDto
    {

        public long Id { get; set; }

        public long UserId { get; set; } 

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Location { get; set; }

        public string Area { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

    }
}
