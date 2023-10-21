using ApproveAppApi.Models.Dto;
using PermitApplicationWebApi.Models;

namespace ApproveAppApi.Services
{
    public class PermitService
    {
        public Permit MapPermitInfoDtoToEntity(PermitInfoDto permitInfo)
        {
            return new Permit
            {
                PermitId = permitInfo.Id,
                UserId = permitInfo.UserId,
            


            };
        }
    }
}
