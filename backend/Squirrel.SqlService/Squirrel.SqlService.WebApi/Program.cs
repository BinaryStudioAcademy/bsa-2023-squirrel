using Squirrel.Core.DAL.Extensions;
using Squirrel.SqlService.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSquirrelCoreContext(builder.Configuration);
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.AddMongoDbService(builder.Configuration);
builder.Services.RegisterCustomServices();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseSquirrelCoreContext();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();