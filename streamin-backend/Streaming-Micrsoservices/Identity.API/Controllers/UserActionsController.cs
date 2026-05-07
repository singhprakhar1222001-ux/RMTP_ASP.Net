using Identity.API.Helpers;
using Identity.Infrastructure.Persistance;
using Identity.SharedKernel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserActionsController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private readonly AppIdentityDbContext _dbContext;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserActionsController(UserManager<AppUser> userManager, AppIdentityDbContext dbContext, RoleManager<IdentityRole> roleManager    )
        {
            _userManager = userManager;
            _dbContext = dbContext;
            this.roleManager = roleManager;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            var user = new AppUser
            {
                UserName = userCreateDto.UserName,
                Email = userCreateDto.Email
            };

            var result = await _userManager.CreateAsync(user, userCreateDto.Password);

            if (!result.Succeeded)
            {
                await transaction.RollbackAsync();
                return BadRequest(result.Errors);
            }

            await _userManager.AddToRoleAsync(user, Roles.Member);
            await _dbContext.SaveChangesAsync();
            transaction.Commit();

            return Ok(new {msg="new User Created Successfully"});
        }
    }
    public class UserCreateDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
