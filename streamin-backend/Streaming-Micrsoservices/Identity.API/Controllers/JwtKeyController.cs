using Identity.Application.Features;
using Identity.Application.Service.Jwt;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static System.Net.WebRequestMethods;

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

        [HttpGet("/.well-known/openid-configuration")]
        public IActionResult OpenID()
        {
            return Ok(new
            {
                issuer= "https://localhost:7056",
                jwks_uri = "https://localhost:7056/.well-known/jwks.json",
                token_endpoint= "https://localhost:7056/login"
            });
        }

        
    }
}
