using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using POC.BLL.Services;
using POC.DAL.Repositories;
using System.Text;

namespace POC.Web.Config
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Add the lifecycle dependecies
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailConfirmService, EmailConfirmService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICanvasService, CanvasService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IInstagramService, InstagramService>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton(typeof(IConfigurationService<>), typeof(ConfigurationService<>));
        }

        public static void IdentityConfiguration(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });
        }

        public static void AuthenticationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JWT").GetValue<string>("Issuer"),
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("JWT").GetValue<string>("Audience"),
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
              configuration.GetSection("JWT").GetValue<string>("Key"))),
                    ValidateIssuerSigningKey = true,
                };
            })
            .AddCertificate();
        }

        /// <summary>
        /// Sets the swagger configuration
        /// </summary>
        /// <param name="services"></param>
        public static void SwashBuckleConfigService(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, Id = "Bearer"
                            }
                        }, new string[] { }
                    }
                });
            });

        }
    }
}