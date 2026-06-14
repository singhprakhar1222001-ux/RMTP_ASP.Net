using Identity.Application.Service.Jwt;
using Identity.SharedKernel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;




namespace Identity.API.Controllers
{
    [ApiController]
    
    [Route("[Controller]")]

    public class LoginController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AppUser> signInManager;
        public LoginController(IJwtService jwtService, IHttpContextAccessor httpContextAccessor, SignInManager<AppUser> signInManager) {
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
            this.signInManager = signInManager;
        }
        
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Login([FromBody] UserLogin? userLogin)
        {

            var httpContext = _httpContextAccessor.HttpContext;
            var header = httpContext.Request.Headers;
            if (header["GrantType"] == "Refresh")
            {
                //validate or invalidate the token
                var cookies = httpContext.Request.Cookies;
                string RefreshToken = cookies["Refresh-Token"];

                var response = await _jwtService.ValidateCookies(RefreshToken);
                if (response.Error!=null)
                {
                    return Unauthorized(response.Error);
                }
                CookieOptions cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    Secure = false
                };
                Response.Cookies.Delete("Refresh-Token");
                Response.Cookies.Append("Refresh-Token",response.Hash, cookieOptions);
                var token = await _jwtService.GenerateToken(response.UserId);
                return Ok(new { token = token });
            }
            else
            {
                var result = await signInManager.PasswordSignInAsync(
                           userLogin.UserName, userLogin.Password, isPersistent: false, lockoutOnFailure: true
                );
                if (!result.Succeeded) {
                    return Unauthorized("Credentials provided were incorrect");
                }
                var token = await _jwtService.GenerateToken(userLogin.UserName);
                string response = await _jwtService.GenerateRefreshToken(userLogin.UserName);
                CookieOptions cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1),
                    Secure = false
                };
                Response.Cookies.Delete("Refresh-Token");
                Response.Cookies.Append("Refresh-Token", response, cookieOptions);
                
                return Ok(new { token = token });

            }

            
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
