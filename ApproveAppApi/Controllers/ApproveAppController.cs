using ApproveAppApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApproveAppApi.Controllers
{
    [Route("api/approve")]
    [ApiController]
    public class ApproveAppController : ControllerBase
    {
        private readonly ApproveAPIDbContext _approveAPIDbContext;
     

        public ApproveAppController(ApproveAPIDbContext approveAPIDbContext)
        {
            _approveAPIDbContext = approveAPIDbContext;

        }

        // POST api/approve/{id}
        [HttpPost("{id}")]
        public async Task<ActionResult> ApproveApplication(long id, string approver)
        {
            
            var existingPermit = await _approveAPIDbContext.Permit.FindAsync(id);

            if (existingPermit != null)
            {
                existingPermit.Status = "Approved";
                existingPermit.UpdatedBy = approver;
                existingPermit.DateTimeUpdated = DateTime.Now;
             
                await _approveAPIDbContext.SaveChangesAsync();
            }
        
            return Ok("Permit is approved successfully.");

        }

        // GET: api/approve/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permit>> GetPermit(long id)
        {
            if (_approveAPIDbContext.Permit == null)
            {
                return NotFound("Unable to find the permit");
            }
            var permit = await _approveAPIDbContext.Permit.FindAsync(id);

            if (permit == null)
            {
                return NotFound("Unable to find the permit");
            }

            return permit;
        }


    }
}
