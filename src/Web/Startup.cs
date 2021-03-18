using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POC.BLL.Services;
using POC.DAL;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddDependencies();
            services.AddDbContext<EFContext>();
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<EFContext>().AddDefaultTokenProviders();
            services.IdentityConfiguration();
            services.AuthenticationConfiguration(_configuration);
            services.SwashBuckleConfigService();
            services.AddCors();
            services.AddHostedService<InstagramHostedService>();
            services.AddControllers().AddNewtonsoftJson(cfg =>
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
