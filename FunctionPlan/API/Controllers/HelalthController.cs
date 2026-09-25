using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HelalthController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetHealth()
        {
            return Ok(new { status= "Healthy" });
        }

    }
}
