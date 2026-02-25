using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.IO;
using System.Text;




namespace Identity.API.Controllers
{
    [ApiController]
    [Route("{Controller}")]

    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            
            
            

            return Ok("Login Successfull");
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
