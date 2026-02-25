using System.Security.Cryptography;

namespace Identity.API.Services
{
    public class LoginService
    {
        public LoginService() { }   

        public async Task CreateKey()
        {
            var rsa = RSA.Create();
            var privateKey = rsa.ExportRSAPrivateKey();
            File.WriteAllBytes("key.txt", privateKey);
        }
    }
}
