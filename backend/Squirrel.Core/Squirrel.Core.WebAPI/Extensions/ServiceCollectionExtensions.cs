using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Squirrel.AzureBlobStorage.Models;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.Services;
using Squirrel.Core.Common.DTO.Auth;
using Squirrel.Core.Common.Interfaces;
using Squirrel.Core.Common.JWT;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace Squirrel.Core.WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterCustomServices(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddScoped<JwtIssuerOptions>();
        services.AddScoped<IJwtFactory, JwtFactory>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IDatabaseItemsService, DatabaseItemsService>();
        services.AddScoped<IProjectDatabaseService, ProjectDatabaseService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IScriptService, ScriptService>();
        services.AddScoped<ICommitChangesService, CommitChangesService>();
        services.AddScoped<IConsoleConnectService, ConsoleConnectService>();
        services.AddScoped<ITableService, TableService>();

        services.AddScoped<IChangeRecordService, ChangeRecordService>();
        services.AddTransient<IDBStructureSaverService, DBStructureSaverService>();

        services.AddSingleton<IHttpClientService, HttpClientService>();

        services.AddUserIdStorage();
    }

    public static void AddUserIdStorage(this IServiceCollection services)
    {
        services.AddScoped<UserIdStorageService>();
        services.AddTransient<IUserIdSetter>(s => s.GetRequiredService<UserIdStorageService>());
        services.AddTransient<IUserIdGetter>(s => s.GetRequiredService<UserIdStorageService>());
    }

    public static void AddValidation(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
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
            options.SecretJwtKey = secretKey;
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

    public static void AddAuthenticationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthenticationSettings>(configuration.GetSection<AuthenticationSettings>());
    }

    public static void ConfigureAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageOptions>(configuration.GetSection<BlobStorageOptions>());
    }


    private static IConfigurationSection GetSection<T>(this IConfiguration configuration)
    {
        return configuration.GetSection(typeof(T).Name);
    }
}