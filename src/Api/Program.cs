using Api.Middlewares;
using Api.ServiceExtensions;
using Application;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.AddSqlServerDbContext<AppDbContext>(connectionName: "ioc");

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationCustomExtensions();
builder.Services.AddInfrastructureCustomExtensions();
builder.Services.AddCustomIdentity();
builder.Services.AddCustomAuthenticationJwt(builder.Configuration);
builder.Services.AddCustomCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// OpenAPI or Swagger, you decide which one to use
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) { }

// OpenAPI or Swagger, you decide which one to use
//app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

// Seed the database with initial data
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await db.Database.EnsureCreatedAsync();

await IdentitySeeder.SeedAsync(scope.ServiceProvider);

app.Run();
