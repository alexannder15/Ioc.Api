using Api.Middlewares;
using Api.ServiceExtensions;
using Application;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
if (builder.Environment.IsDevelopment())
{
    builder.AddSqlServerDbContext<AppDbContext>(connectionName: "ioc");
}
else
{
    var conn = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));
}

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationCustomExtensions();
builder.Services.AddInfrastructureCustomExtensions();
builder.Services.AddCustomIdentity();
builder.Services.AddCustomAuthenticationJwt(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
