using PermitApplicationWebApi.Models;
using PermitApplicationWebApi.Models.Dto;

namespace PermitApplicationWebApi.Services
{
    public class PermitService
    {
        public Permit MapPermitInfoDtoToEntity(PermitInfoDto permitInfo)
        {
            return new Permit
            {
              PermitId = permitInfo.Id,
              UserId = permitInfo.UserId,
              StartDate = permitInfo.StartDate,
              EndDate = permitInfo.EndDate, 
              Location = permitInfo.Location,
              Area = permitInfo.Area,
            

            };
        }
    }
}
