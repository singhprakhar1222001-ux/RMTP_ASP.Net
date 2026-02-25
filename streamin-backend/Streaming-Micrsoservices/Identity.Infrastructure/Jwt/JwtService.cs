using Identity.Application.Features;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Identity.Infrastructure.Jwt
{
    public class JwtService : IJwtService
    {
        public async Task GenerateSecurityKey()
        {
            try
            {
                var rsa = RSA.Create();
                var privateKey = rsa.ExportRSAPrivateKey();
                File.WriteAllBytes("key.txt", privateKey);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public string GenerateToken()
        {
            try
            {
                var rsa = RSA.Create();
                rsa.ImportRSAPublicKey(File.ReadAllBytes("key.txt"),out _);
                var rsaKey = new RsaSecurityKey(rsa);

                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "http://localhost:5000",
                    Audience = "TaskServer",
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("guid",Guid.NewGuid().ToString()),
                        new Claim("userid","demo@mail.com")
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15)
                };

                var tokenhandleer = new JsonWebTokenHandler();
                var token=tokenhandleer.CreateToken(securityTokenDescriptor);
                return token;

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public async Task ValidateToken()
        {
            throw new NotImplementedException();
        }
    }
}
