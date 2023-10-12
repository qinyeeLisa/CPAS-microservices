using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PermitApplicationWebApi.Data;
using PermitApplicationWebApi.Models;

namespace PermitApplicationWebApi.Controllers
{
    [Route("api/permit")]
    [ApiController]
    public class PermitController : ControllerBase
    {
        private readonly PermitAPIDbContext _permitAPIDbContext;

        public PermitController(PermitAPIDbContext permitAPIDbContext)
        {
            _permitAPIDbContext = permitAPIDbContext;
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
        public async Task<ActionResult<Permit>> GetPermit(int id)
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
        public async Task<IActionResult> EditPermit(int id, Permit permit)
        {
            if (id != permit.PermitId)
            {
                return BadRequest();
            }

            _permitAPIDbContext.Entry(permit).State = EntityState.Modified;

            try
            {
                await _permitAPIDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) // to prevent concurrent update and delete
            {
                if (!PermitExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); //will return 204 no content
        }

        // POST: api/permit/createpermit
        [HttpPost("createpermit/")]
        public async Task<ActionResult<Permit>> CreatePermit(int UserId, string Location, string Area, string Status, string CreatedBy, string UpdatedBy)
        {
          

            Permit permit = new Permit
            {
                UserId = UserId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Location = Location,
                Area = Area,
                Status = Status,
                CreatedBy = CreatedBy,
                DateTimeCreated = DateTime.Now,
                UpdatedBy = UpdatedBy,
                DateTimeUpdated = DateTime.Now,


            };

            await _permitAPIDbContext.Permits.AddAsync(permit);
            await _permitAPIDbContext.SaveChangesAsync();
            return Ok("Permit added successfully");
        }

        // DELETE: api/Permit/DeletePermit/5
        [HttpDelete("deletepermit/{id}")]
        public async Task<IActionResult> DeletePermit(int id)
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

        private bool PermitExists(int id)
        {
            return (_permitAPIDbContext.Permits?.Any(e => e.PermitId == id)).GetValueOrDefault();
        }
    }
}
