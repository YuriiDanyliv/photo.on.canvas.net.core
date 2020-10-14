using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POC.DAL.Models;

namespace POC.Web
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public ILogger _logger { get; set; }
    
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<IConfiguration>(Configuration);
      services.AddDependencies();

      services.AddDbContext<DAL.Context.EFContext>();
      services.AddIdentity<DAL.Entities.User, IdentityRole>()
      .AddEntityFrameworkStores<DAL.Context.EFContext>();
      services.AddAutoMapper(typeof(Startup));
      services.IdentityConfiguration();
      services.EmailConfigService(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
      services.SwashBuckleConfigService();
      
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

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
