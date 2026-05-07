using Identity.SharedKernel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features.Users
{
    public record User(string UserName, string Email, string Password);
    public class UserRegistration
    {
        private readonly UserManager<AppUser> _userManager;

        public UserRegistration(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            var appUser = new AppUser
            {
                UserName = user.UserName,
                Email = user.Email
            };
            var result = await _userManager.CreateAsync(appUser, user.Password);
            return result;
        }
    }
}
