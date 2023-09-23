using Microsoft.AspNetCore.SignalR;
using Squirrel.AzureBlobStorage.Extensions;
using Squirrel.Core.BLL.Hubs;
using Squirrel.Core.DAL.Extensions;
using Squirrel.Core.WebAPI.Extensions;
using Squirrel.Shared.Middlewares;
using Squirrel.Shared.Extensions;
using Squirrel.SqlService.WebApi.Extensions;
using Squirrel.SqlService.WebApi.Middlewares;
using Squirrel.SqlService.BLL.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSquirrelCoreContext(builder.Configuration);
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddMongoDbService(builder.Configuration);
builder.Services.AddAzureBlobStorage(builder.Configuration);
builder.Services.AddSignalR();
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
builder.Services.RegisterCustomServices(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://*:5076");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GenericExceptionHandlerMiddleware>();
app.UseMiddleware<SignalRMiddleware>();

app.UseHttpsRedirection();

app.UseSquirrelCoreContext();

app.UseConsoleAppHub();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
