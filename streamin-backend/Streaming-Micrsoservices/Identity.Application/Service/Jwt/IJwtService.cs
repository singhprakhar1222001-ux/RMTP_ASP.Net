using Identity.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Service.Jwt
{
    public interface IJwtService
    {
        public Task GenerateSecurityKey();
        public Task<string> GenerateToken(string UserName);

        public Task ValidateToken();

        public List<JwkDTO> GenerateJwls();

        public Task<string> GenerateRefreshToken(string UserName);

        public Task<ValidateResponse> ValidateCookies(string RefreshSimple);


    }
}
