using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SearchCampsitesApi.Data;
using SearchCampsitesApi.Models;

namespace SearchCampsitesApi.Controllers
{
    [ApiController]
    [Route("api/campsites")]
    public class SearchCampsitesController : ControllerBase
    {
        private readonly CampsiteAPIDbContext _campsitesAPIDbContext;
        
        public SearchCampsitesController(CampsiteAPIDbContext campsiteAPIDbContext)
        {
            _campsitesAPIDbContext = campsiteAPIDbContext;
        }

        private readonly ILogger<SearchCampsitesController> _logger;

        public SearchCampsitesController(ILogger<SearchCampsitesController> logger)
        {
            _logger = logger;
        }

       
    }
}