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
using Identity.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Identity.SharedKernel;

namespace Identity.Infrastructure.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly AppIdentityDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public JwtService(AppIdentityDbContext context, SignInManager<AppUser> userManager)
        {
            _context = context;
            _signInManager = userManager;
        }

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

        public async Task<string> GenerateToken(string UserName)
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
                        new Claim("userid",UserName)
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


        public async Task<ValidateResponse> ValidateCookies(string Refresh)
        {
            string res = string.Empty;
            //string Refresh = GenerateHashfromString(RefreshSimple);
            var existingHash = await _context.RefreshTokens.Where(x => x.TokenHash == Refresh).FirstOrDefaultAsync();

            if (existingHash == null || existingHash.IsRevoked==true || existingHash.ReplacedByHash == Refresh)
            {
                if (existingHash != null)
                {
                    existingHash.IsRevoked = true;
                }
                return new ValidateResponse
                {
                    Error = "Refresh Token string invalid"
                };
            }
            
            //send new refresh token
            string str = GenerateRandomString();
            string hash = GenerateHashfromString(str);

            existingHash.ExpiresOn = DateTime.UtcNow.AddDays(1);
            existingHash.ReplacedByHash = Refresh;
            existingHash.TokenHash = hash;
            await _context.SaveChangesAsync();
            res = hash;
            ValidateResponse validateResponse = new ValidateResponse
            {
                Hash = res,
                UserId=existingHash.UserId,
            };

            return validateResponse;
            
        }
        public async Task<string> GenerateRefreshToken(string UserName)
        {
            string str = GenerateRandomString();
            string hash=GenerateHashfromString(str);

            RefreshTokenStore refreshTokenStore = new RefreshTokenStore
            {
                TokenHash = hash,
                ExpiresOn = DateTime.UtcNow.AddHours(12),
                UserId= UserName
            };
            await _context.RefreshTokens.AddAsync(refreshTokenStore);
            await _context.SaveChangesAsync();
            return hash;
        }

        public string GenerateRandomString()
        {
            var bytes=new byte[64];
            using var rng=RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        public string GenerateHashfromString(string str)
        {
            using var sha = SHA256.Create();
            var bytes=sha.ComputeHash(Encoding.UTF8.GetBytes(str));
            return Convert.ToBase64String(bytes);
        }
    }
}
