using file_uploader.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace file_uploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        private readonly FileUploaderContext _context;

        public FileUploaderController(FileUploaderContext context)
        {
            _context = context;
        }

        // GET: api/FileUploader
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>>  GetUsers ()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET api/FileUploader/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return $"value = {id}";
        }

        // POST api/FileUploader
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/FileUploader/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/FileUploader/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
