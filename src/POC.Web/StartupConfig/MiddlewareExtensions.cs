using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WEB.StartupExtensions
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
  }
}