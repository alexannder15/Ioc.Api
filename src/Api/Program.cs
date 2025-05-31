using Application;
using Infrastructure;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<AppDbContext>(connectionName: "postgresdb");

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationCustomExtensions();
builder.Services.AddInfrastructureCustomExtensions(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    //var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    //// Seed logic
    //if (!await roleManager.RoleExistsAsync("Admin"))
    //    await roleManager.CreateAsync(new IdentityRole("Admin"));

    //var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    //if (adminUser == null)
    //{
    //    adminUser = new IdentityUser
    //    {
    //        UserName = "admin@example.com",
    //        Email = "admin@example.com",
    //    };
    //    await userManager.CreateAsync(adminUser, "YourSecurePassword123!");
    //    await userManager.AddToRoleAsync(adminUser, "Admin");
    //}

    //var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //db.Database.Migrate(); // Applies any pending migrations
}

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
