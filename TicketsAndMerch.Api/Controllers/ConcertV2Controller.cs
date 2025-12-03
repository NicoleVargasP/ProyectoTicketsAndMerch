using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsAndMerch.Api.Controllers
{
    [Route("api/v{version:ApiVersion}/concert")]
    [ApiVersion("2.0")]
    [ApiController]
    public class ConcertV2Controller : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Version = 2.0,
                Message = "Estoy en la version 2 de concert"
            });
        }
    }
}

