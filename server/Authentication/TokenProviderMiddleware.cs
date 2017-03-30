using System;
using System.Linq;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RadzenEndToEndAngularApplication.Authentication
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate next;
        private readonly TokenProviderOptions options;
        private readonly ILogger logger;
        private readonly JsonSerializerSettings serializerSettings;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHostingEnvironment env;

        public TokenProviderMiddleware(
            RequestDelegate next,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<TokenProviderOptions> options,
            ILoggerFactory loggerFactory,
            IHostingEnvironment env)
        {
            this.next = next;
            this.env = env;
            this.logger = loggerFactory.CreateLogger<TokenProviderMiddleware>();
            this.signInManager = signInManager;
            this.userManager = userManager;

            this.options = options.Value;

            ThrowIfInvalidOptions(this.options);

            this.serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Equals(options.LoginPath, StringComparison.Ordinal))
            {
                if (!context.Request.Method.Equals("POST"))
                {
                    context.Response.StatusCode = 400;
                    return context.Response.WriteAsync("Bad request.");
                }

                logger.LogInformation("Handling request: " + context.Request.Path);

                return Login(context);
            }

            if (context.Request.Path.Equals(options.RegisterPath, StringComparison.Ordinal))
            {
                if (!context.Request.Method.Equals("POST"))
                {
                    context.Response.StatusCode = 400;
                    return context.Response.WriteAsync("Bad request.");
                }

                logger.LogInformation("Handling request: " + context.Request.Path);

                return Register(context);
            }

            if (context.Request.Path.Equals(options.TokenPath, StringComparison.Ordinal))
            {
                logger.LogInformation("Handling request: " + context.Request.Path);

                return GenerateToken(context);
            }

            return next(context);
        }

        private async Task CreateJwtToken(HttpContext context, string name, List<Claim> claims)
        {
            var now = DateTime.UtcNow;

            claims.AddRange(new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, name),
                new Claim(JwtRegisteredClaimNames.Jti, await options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            });

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)options.Expiration.TotalSeconds
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
        }

        private async Task Login(HttpContext context)
        {
            var json = new StreamReader(context.Request.Body).ReadToEnd();
            var body = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

            var username = "";
            var password = "";
            body.TryGetValue("username", out username);
            body.TryGetValue("password", out password);

            await Login(context, username, password);
        }

        private async Task WriteError(HttpContext context, string error, int status = 400)
        {
          var response = new { error };

          context.Response.StatusCode = status;
          context.Response.ContentType = "application/json";

          await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }

        private async Task Login(HttpContext context, string username, string password)
        {
            if (username == null || password == null)
            {
                await this.WriteError(context, "Invalid username or password.");
                return;
            }

            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                if (env.EnvironmentName == "Development" && username == "admin" && password == "admin")
                {
                    await CreateJwtToken(context, "admin", new List<Claim>() {
                      new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Administrator"),
                      new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "Admin")
                    });
                    return;
                }

                await this.WriteError(context, "Invalid username or password.");
                return;
            }

            if (!(await userManager.CheckPasswordAsync(user, password)))
            {
                await this.WriteError(context, "Invalid username or password.");
                return;
            }

            var principal = await signInManager.CreateUserPrincipalAsync(user);

            await CreateJwtToken(context, principal.Identity.Name, principal.Claims.ToList());
            return;
        }

        private async Task Register(HttpContext context)
        {
            var json = new StreamReader(context.Request.Body).ReadToEnd();
            var body = JsonConvert.DeserializeObject<IDictionary<string, string>>(json);

            var email = "";
            var password = "";
            body.TryGetValue("email", out email);
            body.TryGetValue("password", out password);

            var user = new IdentityUser { UserName = email, Email = email };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
              await Login(context, email, password);
              return;
            }

            string message = String.Join(Environment.NewLine, result.Errors.Select(error => error.Description));

            await this.WriteError(context, message);
            return;
        }

        private async Task GenerateToken(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                await this.WriteError(context, "Unauthorized", 403);
                return;
            }
            var claims = context.User.Claims.ToList();
            var now = DateTime.UtcNow;

            claims.AddRange(new []
            {
                new Claim(JwtRegisteredClaimNames.Sub, context.User.Identity.Name),
                new Claim(JwtRegisteredClaimNames.Jti, await options.NonceGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            });

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)options.Expiration.TotalSeconds
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
        }

        private static void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.Expiration));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.SigningCredentials));
            }

            if (options.NonceGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.NonceGenerator));
            }
        }

        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();
    }
}
