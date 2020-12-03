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
using POC.DAL.Context;
using POC.DAL.Entities;
using POC.Web.Config;
using POC.Web.Helpers;

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
      .AddEntityFrameworkStores<DAL.Context.EFContext>();

      services.AddAutoMapper(typeof(Startup));
      services.IdentityConfiguration();
      services.SwashBuckleConfigService();

      services.AddCors();

      services.AddAuthentication(
        CertificateAuthenticationDefaults.AuthenticationScheme)
      .AddCertificate();

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
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
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
