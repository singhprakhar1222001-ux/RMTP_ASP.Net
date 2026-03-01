using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Work_Service.API.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Authorize]
    public class WorkController : Controller
    {
        [HttpGet]
        public IActionResult GetWork()
        {
            return Ok("No work");
        }
    }
}
