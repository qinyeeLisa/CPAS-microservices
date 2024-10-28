using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SearchCampsitesApi.Data;
using SearchCampsitesApi.Models;
using System.Runtime.CompilerServices;

namespace SearchCampsitesApi.Controllers
{
    [ApiController]
    [Route("campsitesapi/campsites")]
    public class SearchCampsitesController : ControllerBase
    {
        private readonly CampsiteAPIDbContext _campsitesAPIDbContext;
        private readonly ILogger<SearchCampsitesController> _logger;

        public SearchCampsitesController(CampsiteAPIDbContext campsiteAPIDbContext, ILogger<SearchCampsitesController> logger)
        {
            _campsitesAPIDbContext = campsiteAPIDbContext;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campsites>>> GetCampsites()
        {
            AddCorsHeaders();
            if (_campsitesAPIDbContext.Campsite == null)
            {
                return NotFound();
            }
            return await _campsitesAPIDbContext.Campsite.ToListAsync();


        }


        [HttpGet]
        [Route("SearchCampsites")]
        public async Task<ActionResult<IEnumerable<Campsites>>> SearchCampsites(String Address, String CampsiteName, int? Size, String OwnerName)
        {
            AddCorsHeaders();
            //  List<Campsites> campsitesList=await _campsitesAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            var campsitesList = await _campsitesAPIDbContext.Campsite.FromSql($"EXECUTE uspSearchCampsites @Address={Address}, @CampsiteName={CampsiteName}, @Size={Size}, @OwnerName={OwnerName}").ToListAsync();
            if (campsitesList.Count() == 0)
            {
                return NotFound();
            }
            
            return  campsitesList;
        }
        /*returns the size list in the case frontEnd wants to get the list of sizes available as a dropdown*/
        [HttpGet]
        [Route("GetSizeList")]
        public async Task<ActionResult<IEnumerable<int>>> GetSizeList()
        {
            AddCorsHeaders();
            //  List<Campsites> campsitesList=await _campsitesAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            var sizeList = await _campsitesAPIDbContext.Campsite.FromSql($"SELECT * FROM dbo.Campsites").Select(p=>p.Size).Distinct().ToListAsync();
            if (sizeList.Count() == 0)
            {
                return NotFound();
            }

            return sizeList;
        }

        [HttpPost]
        [Route("CreateCampsites")]
        public async Task<IActionResult> CreateCampsites(int UserId, String Address, String CampsiteName, int Size, String Remarks, String OwnerName)
        {
            AddCorsHeaders();
            Campsites campsite = new Campsites
            {
                UserId = UserId,
                Address = Address,
                CampsiteName= CampsiteName,
                Size=Size,
                Remarks=Remarks,
                CreatedBy=OwnerName,
                UpdatedBy=OwnerName,
                DateTimeCreated = DateTime.Now,
                DateTimeUpdated = DateTime.Now


            };
            //  List<Campsites> campsitesList=await _campsitesAPIDbContext.Campsite.Where<Campsites>(site => site.Address..Contains(Address) && site.CampsiteName.Contains(CampsiteName) && site.Size==Size && site.CreatedBy.Contains(OwnerName)).ToListAsync();
            await _campsitesAPIDbContext.Campsite.AddAsync(campsite);
            await _campsitesAPIDbContext.SaveChangesAsync();
            return Ok("Campsite added successfully");
        }

        [HttpDelete("DeleteCampsite")]
        //[ProducesResponseType(typeof(ErrorModel), 500)]
        public async Task<IActionResult> DeleteCampsite(int campsiteId)
        {
            AddCorsHeaders();
            var campSite = await _campsitesAPIDbContext.Campsite.Where(u => u.CampsiteId == campsiteId).FirstOrDefaultAsync();
            if (campSite != null)
            {
                _campsitesAPIDbContext.Campsite.Remove(campSite);
                await _campsitesAPIDbContext.SaveChangesAsync();
                return Ok("Campsite is deleted successfully.");
            }
            else
            {
                return NotFound("Unable to delete campSite.");
            }
        }

        private void AddCorsHeaders()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS, DELETE");
            HttpContext.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        }


    }


}