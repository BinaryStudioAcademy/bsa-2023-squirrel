﻿using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Squirrel.Core.BLL.Interfaces;
using Squirrel.Core.BLL.MappingProfiles;
using Squirrel.Core.BLL.Services;
using Squirrel.Core.Common.Models;
using Squirrel.Core.DAL.Context;
using Squirrel.Core.WebAPI.Validators;
using System.Reflection;

namespace Squirrel.Core.WebAPI.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddTransient<ISampleService, SampleService>();

            services.Configure<MongoDatabaseConnectionSettings>(
                configuration.GetSection("MongoDatabase"));

            services.AddScoped(typeof(IMongoService<>), typeof(MongoService<>));
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
    }
}
