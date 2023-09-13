using Microsoft.Extensions.FileProviders;
using Squirrel.AzureBlobStorage.Extensions;
using Squirrel.Core.BLL.Extensions;
using Squirrel.Core.DAL.Extensions;
using Squirrel.Core.WebAPI.Extensions;
using Squirrel.Core.WebAPI.Middlewares;
using Squirrel.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", reloadOnChange: true, optional: true)
    .AddEnvironmentVariables()
    .Build();

builder.AddSerilog();

builder.Services.AddControllers();
builder.Services.AddSquirrelCoreContext(builder.Configuration);
builder.Services.AddAuthenticationSettings(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterCustomServices();
builder.Services.AddAutoMapper();
builder.Services.AddSwaggerGen();
builder.Services.AddValidation();
builder.Services.ConfigureJwtAuth(builder.Configuration);
builder.Services.AddAzureBlobStorage(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddHealthChecks();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.WebHost.UseUrls("http://*:5050");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GenericExceptionHandlerMiddleware>();

app.UseSquirrelCoreContext();

app.UseCors(opt => opt
    .AllowAnyHeader()
    .WithExposedHeaders("Content-Disposition")
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .WithExposedHeaders("Token-Expired"));

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CurrentUserMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health");
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
