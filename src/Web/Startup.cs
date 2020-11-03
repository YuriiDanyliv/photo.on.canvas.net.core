using System.IO;
using AutoMapper;
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
    public ILogger _logger { get; set; }

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
      services.AddControllers()
      .AddNewtonsoftJson(cfg =>
      cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.ConfigureExceptionHandler(logger);
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseCors(
        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
      );
      app.UseStaticFiles();
      app.UseStaticFiles(new StaticFileOptions()
      {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
        RequestPath = new PathString("/Resources")
      });
      app.UseAuthorization();
      app.SwaggerMiddleware();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
