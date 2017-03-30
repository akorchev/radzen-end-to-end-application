using System;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Builder;
using Microsoft.IdentityModel.Tokens;

using RadzenEndToEndAngularApplication.Data;
using RadzenEndToEndAngularApplication.Authentication;

namespace RadzenEndToEndAngularApplication
{
  public class Startup
  {
    private static void DotEnv([CallerFilePath] string path = "")
    {
      var dotEnv = Path.Combine(Path.GetDirectoryName(path), "..", ".env");

      if (File.Exists(dotEnv))
      {
        var dotenv = File.ReadAllText(dotEnv).Trim();
        var lines = dotenv.Split('\n');

        foreach (var line in lines)
        {
          var index = line.IndexOf("=");

          var key = line.Substring(0, index);

          var value = line.Substring(index + 1);

          Environment.SetEnvironmentVariable(key, value.TrimStart('"').TrimEnd('"'));
        }
      }
    }

    public Startup(IHostingEnvironment env)
    {
      Startup.DotEnv();

      var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        .AddEnvironmentVariables();

      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors();

      services.AddMvcCore()
              .AddJsonFormatters();

      services.AddOData();

      services.AddIdentity<IdentityUser, IdentityRole>()
              .AddEntityFrameworkStores<TestContext>();

      services.AddDbContext<TestContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("TestConnection"));
      });
    }

    public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      var provider = app.ApplicationServices.GetRequiredService<IAssemblyProvider>();

      app.UseCors(builder =>
        builder.WithOrigins("*")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .AllowAnyOrigin()
      );

      var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("RadzenEndToEndAngularApplicationSecretSecurityKeyRadzenEndToEndAngularApplication"));

      app.UseJwtTokenProvider(new TokenProviderOptions
      {
          Audience = "RadzenEndToEndAngularApplicationAudience",
          Issuer = "RadzenEndToEndAngularApplication",
          SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
      });

      var tokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = signingKey,
          ValidateIssuer = true,
          ValidIssuer = "RadzenEndToEndAngularApplication",
          ValidateAudience = true,
          ValidAudience = "RadzenEndToEndAngularApplicationAudience",
          ValidateLifetime = true,
          ClockSkew = TimeSpan.Zero
      };

      var options = new JwtBearerOptions
      {
          AutomaticAuthenticate = true,
          AutomaticChallenge = true,
          TokenValidationParameters = tokenValidationParameters
      };

      app.UseJwtBearerAuthentication(options);

      app.UseMvc(builder =>
      {
        var testBuilder = new ODataConventionModelBuilder(provider);
        testBuilder.ContainerName = "TestContext";

        testBuilder.EntitySet<RadzenEndToEndAngularApplication.Models.Test.Order>("Orders");
        testBuilder.EntitySet<RadzenEndToEndAngularApplication.Models.Test.OrderDetail>("OrderDetails");
        testBuilder.EntitySet<RadzenEndToEndAngularApplication.Models.Test.Product>("Products");

        builder.MapODataRoute("odata/Test", testBuilder.GetEdmModel());
      });

      Bootstrapper.EnsureIdentitySchema(app.ApplicationServices.GetRequiredService<TestContext>());

      await Bootstrapper.CreateRoles(app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>());
    }
  }
}
