using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.SharedKernel
{
    public class AppUser : IdentityUser
    {
        public bool EnableNotifications { get; set; }
    }
}
