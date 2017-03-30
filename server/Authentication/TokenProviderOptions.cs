using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace RadzenEndToEndAngularApplication.Authentication
{
    public class TokenProviderOptions
    {
        public string LoginPath { get; set; } = "/auth/login";
        public string TokenPath { get; set; } = "/auth/token";
        public string RegisterPath { get; set; } = "/auth/register";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(5);
        public SigningCredentials SigningCredentials { get; set; }
        public Func<Task<string>> NonceGenerator { get; set; }
            = new Func<Task<string>>(() => Task.FromResult(Guid.NewGuid().ToString()));
    }
}
