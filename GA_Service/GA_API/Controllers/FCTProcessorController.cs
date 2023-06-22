using BLService;
using Microsoft.AspNetCore.Mvc;

namespace GA_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FCTProcessorController : ControllerBase
    {

        private readonly ILogger<FCTProcessorController> _logger;
        private readonly IBLService<TicketDto> servicio;

        public FCTProcessorController(ILogger<FCTProcessorController> logger, IBLService<TicketDto> servicio, IConfiguration config)
        {
            _logger = logger;
            this.servicio = servicio;
            this.servicio.connstr = config.GetConnectionString("SQLConn") ?? string.Empty;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketDto data)
        {
            var resultado = await servicio.Save(data);
            if (resultado != 0)
                return Ok(new { resultado });
            else
                return BadRequest(new { resultado });
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var resultado = await servicio.GetAll();
            return Ok(new { resultado });
        }
    }
}