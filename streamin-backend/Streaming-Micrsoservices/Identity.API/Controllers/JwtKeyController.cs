using Identity.Application.Features;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Identity.API.Controllers
{
    [ApiController]
    
    public class JwtKeyController : Controller
    {
        private readonly IJwtService jwtService;
        public JwtKeyController(IJwtService jwtService) {
            this.jwtService = jwtService;   
        }

        [HttpGet("/.well-known/jwks.json")]
        public IActionResult GetJwk()
        {
            var res = jwtService.GenerateJwls();
            return Ok(new { Keys = res });
        }
        
    }
}
