using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Identity.Application.Features;




namespace Identity.API.Controllers
{
    [ApiController]
    
    [Route("[Controller]")]

    public class LoginController : Controller
    {
        private readonly IJwtService _jwtService;
        public LoginController(IJwtService jwtService) {
            _jwtService = jwtService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Login()
        {

            var token=_jwtService.GenerateToken();
            

            return Ok(new { token=token});
        }
    }

    public class UserLogin
    {
        public string UserName
        {
            get; set;
        }

        public string Password { get; set; }
    }
}
