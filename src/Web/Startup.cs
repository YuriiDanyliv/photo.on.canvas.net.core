using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using POC.DAL.Context;
using POC.DAL.Entities;
using POC.Web.Config;
using POC.Web.Helpers;
using System.Text;
using POC.BLL.Services;

namespace POC.Web
{
  public class Startup
  {
    public Startup()
    {
      var builder = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json");

      _configuration = builder.Build();
    }

    public IConfiguration _configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IConfiguration>(_configuration);
      services.AddDependencies();

      services.AddDbContext<EFContext>();
      services.AddIdentity<User, IdentityRole>()
      .AddEntityFrameworkStores<DAL.Context.EFContext>()
      .AddDefaultTokenProviders();
      services.IdentityConfiguration();

      services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options => {
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidIssuer = _configuration.GetSection("JWT").GetValue<string>("Issuer"),
          ValidateAudience = true,
          ValidAudience = _configuration.GetSection("JWT").GetValue<string>("Audience"),
          ValidateLifetime = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("JWT").GetValue<string>("Key"))),
          ValidateIssuerSigningKey = true,
        };
      })
      .AddCertificate();

      services.AddAutoMapper(typeof(Startup));
      services.SwashBuckleConfigService();

      services.AddCors();

      services.AddHostedService<InstagramHostedService>();

      services.AddControllers()
      .AddNewtonsoftJson(cfg =>
      cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }


    public void Configure(
      IApplicationBuilder app, 
      IWebHostEnvironment env, 
      ILoggerFactory loggerFactory,
      ILogger<Startup> logger)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // var logger = loggerFactory
      // .AddFile(
      //   Path.Combine(Directory.GetCurrentDirectory(),
      //  _configuration.GetSection("Logging").GetValue<string>("LogPath"))
      // )
      // .CreateLogger("Startup");

      app.ConfigureExceptionHandler(logger);
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseCors(
        builder => builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
      );

      app.StaticFilesConfiguration();

      app.UseAuthentication();
      app.UseAuthorization();

      app.SwaggerMiddleware();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
