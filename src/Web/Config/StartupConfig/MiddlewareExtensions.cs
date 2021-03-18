using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Net;

namespace POC.Web.Config
{
    public static class MiddlewareExtensions
    {
        public static void SwaggerMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });
        }

        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
          {
                  context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                  context.Response.ContentType = "application/json";
                  var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                  if (contextFeature != null)
                  {
                      logger.LogError($"Something went wrong: {contextFeature.Error}");
                      await context.Response.WriteAsync(new
                      {
                          StatusCode = context.Response.StatusCode,
                          Message = contextFeature.Error.Message,
                          source = contextFeature.Error.Source
                      }.ToString());
                  }
              });
            });
        }

        public static void StaticFilesConfiguration(this IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Resources");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = new PathString("/Resources")
            });
        }
    }
}