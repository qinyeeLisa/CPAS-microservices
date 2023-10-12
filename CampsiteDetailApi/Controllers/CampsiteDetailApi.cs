using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CampsiteDetailApi.Data;
using CampsiteDetailApi.Models;
using System.Runtime.CompilerServices;

namespace CampsiteDetailApi.Controllers
{
    [ApiController]
    [Route("api/campsiteDetail")]
    public class CampsiteDetailApiController : ControllerBase
    {
        private readonly CampsiteDetailAPIDbContext _campsiteDetailAPIDbContext;
        private readonly ILogger<CampsiteDetailApiController> _logger;

        public CampsiteDetailApiController(CampsiteDetailAPIDbContext CampsiteDetailAPIDbContext, ILogger<CampsiteDetailApiController> logger)
        {
            _campsiteDetailAPIDbContext = CampsiteDetailAPIDbContext;
            _logger = logger;
        }



        [HttpGet]
        [Route("CampsiteDetail")]
        public async Task<ActionResult<CampsiteDetail>> GetCampsiteDetail(int campsiteDetailId)
        {
            var campsiteDetail = await _campsiteDetailAPIDbContext.CampsiteDetail.Where(u => u.CampsiteDetailId == campsiteDetailId).FirstOrDefaultAsync();
            if (campsiteDetail == null)
            {
                return NotFound();
            }

            return campsiteDetail;
        }
        /*returns the size list in the case frontEnd wants to get the list of sizes available as a dropdown*/
       

        [HttpPost]
        [Route("CreateCampsites")]
        public async Task<IActionResult> CreateCampsiteDetail(int CampsiteId, String AreaName, String OwnerName)
        {
            CampsiteDetail campsiteDetail = new CampsiteDetail
            {
                CampsiteId = CampsiteId,
                AreaName = AreaName,
                CreatedBy = OwnerName,
                UpdatedBy = OwnerName,
                DateTimeCreated = DateTime.Now,
                DateTimeUpdated = DateTime.Now


            };
            //  List<Campsites> campsitesList=await _campsiteDetailAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            await _campsiteDetailAPIDbContext.CampsiteDetail.AddAsync(campsiteDetail);
            await _campsiteDetailAPIDbContext.SaveChangesAsync();
            return Ok("Campsite Detail added successfully");
        }

        [HttpPut]
        [Route("UpdateCampsites")]
        public async Task<IActionResult> UpdateCampsiteDetail(int CampsiteDetailId, String AreaName, String OwnerName)
        {
            var currentCampsiteDetail = await _campsiteDetailAPIDbContext.CampsiteDetail.Where(u => u.CampsiteDetailId == CampsiteDetailId).FirstOrDefaultAsync();

            currentCampsiteDetail.CampsiteDetailId = currentCampsiteDetail.CampsiteDetailId;
            currentCampsiteDetail.CampsiteId = currentCampsiteDetail.CampsiteId;
            currentCampsiteDetail.AreaName = AreaName;
            currentCampsiteDetail.CreatedBy = currentCampsiteDetail.CreatedBy;
            currentCampsiteDetail.UpdatedBy = OwnerName;
            currentCampsiteDetail.DateTimeCreated = currentCampsiteDetail.DateTimeCreated;
            currentCampsiteDetail.DateTimeUpdated = DateTime.Now;
            //  List<Campsites> campsitesList=await _campsiteDetailAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            _campsiteDetailAPIDbContext.Entry(currentCampsiteDetail).State = EntityState.Detached;
            _campsiteDetailAPIDbContext.Entry(currentCampsiteDetail).State = EntityState.Modified;
            await _campsiteDetailAPIDbContext.SaveChangesAsync();
            return Ok("Campsite Detail Updated successfully");
        }

        [HttpDelete]
        //[ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> DeleteCampsiteDetail(int campsiteDetailId)
        {
            var campsiteDetail = await _campsiteDetailAPIDbContext.CampsiteDetail.Where(u => u.CampsiteDetailId == campsiteDetailId).FirstOrDefaultAsync();
            if (campsiteDetail != null)
            {
                _campsiteDetailAPIDbContext.CampsiteDetail.Remove(campsiteDetail);
                await _campsiteDetailAPIDbContext.SaveChangesAsync();
                return Ok("Campsite Detail is deleted successfully.");
            }
            else
            {
                return NotFound("Unable to delete campSite.");
            }
        }


    }
}