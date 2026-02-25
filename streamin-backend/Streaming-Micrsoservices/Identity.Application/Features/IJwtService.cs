using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Features
{
    public interface IJwtService
    {
        public Task GenerateSecurityKey();
        public string GenerateToken();

        public Task ValidateToken();


    }
}
