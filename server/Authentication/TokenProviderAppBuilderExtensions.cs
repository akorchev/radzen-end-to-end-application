using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace RadzenEndToEndAngularApplication.Authentication
{
    public static class TokenProviderAppBuilderExtensions
    {
        public static IApplicationBuilder UseJwtTokenProvider(this IApplicationBuilder app, TokenProviderOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options));
        }
    }
}
