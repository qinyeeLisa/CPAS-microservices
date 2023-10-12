using Microsoft.AspNetCore.Mvc;



namespace EnquiryAppStatusApi.Controllers
{
    [Route("api/enquiryappstatus")]
    [ApiController]
    public class EnquiryAppStatusController : ControllerBase
    {
        // GET: api/<EnquiryAppStatusController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EnquiryAppStatusController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EnquiryAppStatusController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EnquiryAppStatusController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EnquiryAppStatusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
