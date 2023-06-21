using Microsoft.AspNetCore.Mvc;

namespace GA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FCTProcessorController : ControllerBase
    {

        private readonly ILogger<FCTProcessorController> _logger;

        public FCTProcessorController(ILogger<FCTProcessorController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            
        }
    }
}