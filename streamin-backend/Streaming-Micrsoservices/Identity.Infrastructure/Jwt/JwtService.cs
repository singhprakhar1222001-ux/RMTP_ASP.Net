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
using Identity.Application.DTO;

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
                File.WriteAllBytes("key", privateKey);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public string GenerateToken()
        {
            try
            {
                if (!File.Exists("key"))
                {
                    GenerateSecurityKey();
                }
                
                var rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(File.ReadAllBytes("key"),out _);
                var rsaKey = new RsaSecurityKey(rsa);

                SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = "https://localhost:7056",
                    Audience = "workservice",
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("guid",Guid.NewGuid().ToString()),
                        new Claim("userid","demo@mail.com")
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials=new SigningCredentials(rsaKey,SecurityAlgorithms.RsaSha256)
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

        public List<JwkDTO> GenerateJwls()
        {
            try
            {
                var rsa = RSA.Create();
                rsa.ImportRSAPrivateKey(File.ReadAllBytes("key"),out _);
                var parameters=rsa.ExportParameters(false);
                List<JwkDTO> res = new List<JwkDTO>();
                var key = new JwkDTO
                {
                    Kty = "RSA",
                    Kid = Guid.NewGuid().ToString(),
                    Use = "sig",
                    N = Base64UrlEncoder.Encode(parameters.Modulus),
                    E = Base64UrlEncoder.Encode(parameters.Exponent)
                };
                res.Add(key);
                return res;
            }
            catch (Exception ex) { 
                throw ex;
            }
        }
    }
}
