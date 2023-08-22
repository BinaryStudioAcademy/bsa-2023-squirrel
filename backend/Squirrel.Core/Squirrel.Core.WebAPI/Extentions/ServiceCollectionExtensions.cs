using Squirrel.Core.BLL.MappingProfiles;
using Squirrel.Core.BLL.Services;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.WebAPI.Validators;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.Common.Interfaces;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Squirrel.Core.Common.JWT;

namespace Squirrel.Core.WebAPI.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<JwtIssuerOptions>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddTransient<ISampleService, SampleService>();
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(SampleProfile)));
        }

        public static void AddValidation(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<NewSampleDtoValidator>());
        }

        public static void AddSquirrelCoreContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionsString = configuration.GetConnectionString("SquirrelCoreDBConnection");
            services.AddDbContext<SquirrelCoreContext>(options =>
                options.UseSqlServer(
                    connectionsString,
                    opt => opt.MigrationsAssembly(typeof(SquirrelCoreContext).Assembly.GetName().Name)));
        }
        
        public static void ConfigureJwtAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions))!;
            // Get secret key from appsettings for testing.
            var secretKey = jwtAppSettingOptions[nameof(JwtIssuerOptions.SecretJwtKey)];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey!));
            
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)]!;
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)]!;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                    configureOptions.TokenValidationParameters = tokenValidationParameters;
                    configureOptions.SaveToken = true;
                    configureOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
