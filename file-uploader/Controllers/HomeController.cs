using Microsoft.AspNetCore.Mvc;

namespace file_uploader.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "HomeEndpoint")]
        public IActionResult Get()
        {
            return Ok("Hello World!!!");
        }
    }
}