using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PermitApplicationWebApi.Data;
using PermitApplicationWebApi.Models;

namespace PermitApplicationWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PermitController : ControllerBase
    {
        private readonly PermitAPIDbContext _context;

        public PermitController(PermitAPIDbContext context)
        {
            _context = context;
        }

        // GET: api/Permit
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permit>>> GetPermits()
        {
          if (_context.Permits == null)
          {
              return NotFound();
          }
            return await _context.Permits.ToListAsync();
        }

        // GET: api/Permit/GetPermit/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permit>> GetPermit(int id)
        {
          if (_context.Permits == null)
          {
              return NotFound();
          }
            var permit = await _context.Permits.FindAsync(id);

            if (permit == null)
            {
                return NotFound();
            }

            return permit;
        }

        // PUT: api/Permit/PutPermit/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermit(int id, Permit permit)
        {
            if (id != permit.Id)
            {
                return BadRequest();
            }

            _context.Entry(permit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/Permit/PostPermit
        [HttpPost]
        public async Task<ActionResult<Permit>> PostPermit(Permit permit)
        {
          if (_context.Permits == null)
          {
              return Problem("Entity set 'PermitAPIDbContext.Permits'  is null.");
          }
            _context.Permits.Add(permit);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermit", new { id = permit.Id }, permit);
        }

        // DELETE: api/Permit/DeletePermit/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermit(int id)
        {
            if (_context.Permits == null)
            {
                return NotFound();
            }
            var permit = await _context.Permits.FindAsync(id);
            if (permit == null)
            {
                return NotFound();
            }

            _context.Permits.Remove(permit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermitExists(int id)
        {
            return (_context.Permits?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
