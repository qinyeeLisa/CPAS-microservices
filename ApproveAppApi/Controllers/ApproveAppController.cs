using ApproveAppApi.Data;
using ApproveAppApi.Models.Dto;
using ApproveAppApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApproveAppApi.Controllers
{
    [Route("approveapi/approve")]
    [ApiController]
    public class ApproveAppController : ControllerBase
    {
        private readonly ApproveAPIDbContext _approveAPIDbContext;
        private readonly PermitService _permitService;


        public ApproveAppController(ApproveAPIDbContext approveAPIDbContext, PermitService permitService)
        {
            _approveAPIDbContext = approveAPIDbContext;
            _permitService = permitService;
        }

        // POST api/approve/
        [HttpPost]
        public async Task<ActionResult> ApproveApplication([FromBody] PermitInfoDto permitInfo)
        {

            //var getUserID = await _approveAPIDbContext.Permits.FindAsync(permitInfo.UserId);
            //if (getUserID == null)
            //{
            //    return NotFound();
            //}
            try
            {
                var existingPermit = await _approveAPIDbContext.Permits.Where(u => u.PermitId == permitInfo.Id).FirstOrDefaultAsync();

                if (existingPermit != null)
                {

                    var approvedPermit = _permitService.MapPermitInfoDtoToEntity(permitInfo);

                    existingPermit.Status = "Approved";
                    existingPermit.UpdatedBy = approvedPermit.UpdatedBy;
                    existingPermit.DateTimeUpdated = DateTime.Now;

                    await _approveAPIDbContext.SaveChangesAsync();
                    return Ok("Permit is approved successfully.");
                }

                else
                {
                    return Conflict("Permit not approved successfully");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error approving permit: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }

        }


        [HttpPost("rejectpermit")]
        public async Task<ActionResult> RejectApplication([FromBody] PermitInfoDto permitInfo)
        {
            try
            {
                var existingPermit = await _approveAPIDbContext.Permits.Where(u => u.PermitId == permitInfo.Id).FirstOrDefaultAsync();

                if (existingPermit != null)
                {

                    var approvedPermit = _permitService.MapPermitInfoDtoToEntity(permitInfo);

                    existingPermit.Status = "Rejected";
                    existingPermit.UpdatedBy = approvedPermit.UpdatedBy;
                    existingPermit.DateTimeUpdated = DateTime.Now;

                    await _approveAPIDbContext.SaveChangesAsync();
                    return Ok("Permit is rejected successfully.");
                }

                else
                {
                    return Conflict("Permit not rejected successfully");
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error approving permit: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


        // POST api/approve/{id}
        //[HttpPost("{id}")]
        //public async Task<ActionResult> ApproveApplication(long id, string approver)
        //{

        //    var existingPermit = await _approveAPIDbContext.Permit.FindAsync(id);

        //    if (existingPermit != null)
        //    {
        //        existingPermit.Status = "Approved";
        //        existingPermit.UpdatedBy = approver;
        //        existingPermit.DateTimeUpdated = DateTime.Now;

        //        await _approveAPIDbContext.SaveChangesAsync();
        //    }

        //    return Ok("Permit is approved successfully.");

        //}

        // GET: api/approve/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permit>> GetPermit(long id)
        {
            if (_approveAPIDbContext.Permits == null)
            {
                return NotFound("Unable to find the permit");
            }
            var permit = await _approveAPIDbContext.Permits.FindAsync(id);

            if (permit == null)
            {
                return NotFound("Unable to find the permit");
            }

            return permit;
        }


    }
}
