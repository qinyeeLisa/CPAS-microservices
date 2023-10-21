using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PermitApplicationWebApi.Data;
using PermitApplicationWebApi.Models;
using PermitApplicationWebApi.Models.Dto;
using PermitApplicationWebApi.Services;

namespace PermitApplicationWebApi.Controllers
{
    [Route("permitapi/permit")]
    [ApiController]
    public class PermitController : ControllerBase
    {
        private readonly PermitAPIDbContext _permitAPIDbContext;
        private readonly PermitService _permitService;
       
       

        public PermitController(PermitAPIDbContext permitAPIDbContext, PermitService permitService)
        {
            _permitAPIDbContext = permitAPIDbContext;
            _permitService = permitService;
        }

        // GET: api/permit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permit>>> GetPermits()
        {
            if (_permitAPIDbContext.Permits == null)
            {
                return NotFound();
            }
            return await _permitAPIDbContext.Permits.ToListAsync();


        }


        // GET: api/permit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permit>> GetPermit(long id)
        {
          if (_permitAPIDbContext.Permits == null)
          {
              return NotFound();
          }
            var permit = await _permitAPIDbContext.Permits.FindAsync(id);

            if (permit == null)
            {
                return NotFound();
            }

            return permit;
        }


        // PUT: api/permit/editpermit/5
        [HttpPut("editpermit/{id}")]
        public async Task<IActionResult> EditPermit([FromBody] PermitInfoDto permitInfo)
        {
            var getUserID = await _permitAPIDbContext.Users.FindAsync(permitInfo.UserId);
            if (getUserID == null)
            {
                return NotFound();
            }

            var currentPermit = await _permitAPIDbContext.Permits.Where(u => u.PermitId == permitInfo.Id).FirstOrDefaultAsync();
            if (currentPermit != null) 
            {
                var updatedPermit = _permitService.MapPermitInfoDtoToEntity(permitInfo);

                currentPermit.StartDate = updatedPermit.StartDate;
                currentPermit.EndDate = updatedPermit.EndDate;
                currentPermit.Location = updatedPermit.Location;
                currentPermit.Area = updatedPermit.Area;
                updatedPermit.CreatedBy = currentPermit.CreatedBy;
                updatedPermit.DateTimeCreated = currentPermit.DateTimeCreated;
                currentPermit.UpdatedBy = getUserID.Username;
                currentPermit.DateTimeUpdated = DateTime.Now;
                
                _permitAPIDbContext.Entry(currentPermit).State = EntityState.Detached;
                _permitAPIDbContext.Entry(currentPermit).State = EntityState.Modified;
                await _permitAPIDbContext.SaveChangesAsync();
                return Ok(currentPermit);
            }
            else
            {
                return NotFound("Unable to update permit.");
            }
        }


        // PUT: api/permit/editpermit/5
        //[HttpPut("editpermit/{id}")]
        //public async Task<IActionResult> EditPermit(long id, Permit permit)
        //{
        //    if (id != permit.PermitId)
        //    {
        //        return BadRequest();
        //    }

        //    _permitAPIDbContext.Entry(permit).State = EntityState.Modified;

        //    try
        //    {
        //        await _permitAPIDbContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException) // to prevent concurrent update and delete
        //    {
        //        if (!PermitExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent(); //will return 204 no content
        //}



        // POST: api/permit/createpermit
        [HttpPost("createpermit")]
        public async Task<ActionResult<Permit>> CreatePermit([FromBody] PermitInfoDto permitInfo)
        {

            var isPermitExist = await _permitAPIDbContext.Permits.Where(u => u.PermitId == permitInfo.Id).FirstOrDefaultAsync();

            if (isPermitExist == null)
            {
                var getUserID = await _permitAPIDbContext.Users.FindAsync(permitInfo.UserId);
                if (getUserID == null)
                {
                    return NotFound();
                }

                var newPermit = _permitService.MapPermitInfoDtoToEntity(permitInfo);

                newPermit.Status = "Pending";
                newPermit.CreatedBy = getUserID.Username;
                newPermit.DateTimeCreated = DateTime.Now;
                newPermit.UpdatedBy = getUserID.Username;
                newPermit.DateTimeUpdated = DateTime.Now;


                await _permitAPIDbContext.Permits.AddAsync(newPermit);
                await _permitAPIDbContext.SaveChangesAsync();
                return Ok("Permit added successfully");

            }
            else
            {
                return Conflict("Permit exist.");
            }

            
        }






        // DELETE: api/Permit/DeletePermit/5
        [HttpDelete("deletepermit/{id}")]
        public async Task<IActionResult> DeletePermit(long id)
        {
            if (_permitAPIDbContext.Permits == null)
            {
                return NotFound();
            }
            var permit = await _permitAPIDbContext.Permits.FindAsync(id);
            if (permit == null)
            {
                return NotFound();
            }

            _permitAPIDbContext.Permits.Remove(permit);
            await _permitAPIDbContext.SaveChangesAsync();

            return Ok("Permit is deleted successfully");
        }

        private bool PermitExists(long id)
        {
            return (_permitAPIDbContext.Permits?.Any(e => e.PermitId == id)).GetValueOrDefault();
        }
    }
}
