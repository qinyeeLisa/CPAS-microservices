using EnquiryAppStatusApi.Data;
using EnquiryAppStatusApi.Models;
using EnquiryAppStatusApi.Models.Dto;
using EnquiryAppStatusApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnquiryAppStatusApi.Controllers
{
    [Route("api/enquiryappstatus")]
    [ApiController]
    public class EnquiryAppStatusController : ControllerBase
    {

        private readonly EnquiryAPIDbContext _enquiryAPIDbContext;
        private readonly EnquiryService _enquiryService;

        public EnquiryAppStatusController(EnquiryAPIDbContext enquiryAPIDbContext, EnquiryService enquiryService)
        {
            _enquiryAPIDbContext = enquiryAPIDbContext;
            _enquiryService = enquiryService;
        }

        // POST api/enquiryappstatus/createenquiry
        [HttpPost("createenquiry")]
        public async Task<IActionResult> CreateEnquiry([FromBody] EnquiryInfoDto enquiryInfoDto)
        {
            var newEnquiry = _enquiryService.MapEnquiryInfoDtoToEntity(enquiryInfoDto);
            await _enquiryAPIDbContext.Enquiries.AddAsync(newEnquiry);
            await _enquiryAPIDbContext.SaveChangesAsync();
            return Ok("Enquiry is submitted successfully.");
        }


        // GET: api/enquiryappstatus/getenquiry/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Enquiry>> GetEnquiry(long id)
        {
            if (_enquiryAPIDbContext.Enquiries == null)
            {
                return NotFound("Unable to get enquiry info.");
            }
            var enquiry = await _enquiryAPIDbContext.Enquiries.FindAsync(id);

            if (enquiry == null)
            {
                return NotFound("Unable to get enquiry info.");
            }

            return enquiry;
        }



        // GET: api/enquiryappstatus/deleteenquiry/{id}
        [HttpDelete("deleteenquiry/{id}")]
        public async Task<IActionResult> DeleteEnquiry(long id)
        {
            var currentEnquiry = await _enquiryAPIDbContext.Enquiries.Where(u => u.EnquiryId == id).FirstOrDefaultAsync();
            if (currentEnquiry != null)
            {
                _enquiryAPIDbContext.Enquiries.Remove(currentEnquiry);
                await _enquiryAPIDbContext.SaveChangesAsync();
                return Ok("Enquiry is deleted successfully.");
            }
            else
            {
                return NotFound("Unable to delete enquiry.");
            }
        }
    }
}
