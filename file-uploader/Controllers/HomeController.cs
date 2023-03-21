using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace file_uploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/Home
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/Home/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value = {id}";
        }

        // POST api/Home
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Home/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Home/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
