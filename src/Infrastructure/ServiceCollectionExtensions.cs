using System.Text;
using Application.Exceptions;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureCustomExtensions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        //services.AddDbContext<AppDbContext>(options =>
        //    options.UseNpgsql(configuration.GetConnectionString("SqlDataBase"))
        //);

        services
            .AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AppDbContext>();
        //.AddDefaultUI()
        //.AddDefaultTokenProviders();

        services
            .AddAuthentication(opts =>
            {
                opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                var secret = configuration.GetSection("JwtConfig:Secret").Value;
                var issuer = configuration.GetSection("JwtConfig:Issuer").Value;
                var audience = configuration.GetSection("JwtConfig:Audience").Value;

                if (secret == null || issuer == null || audience == null)
                    throw new UnhandledException("Something was wrong with AddJwtBearer JwtConfig");

                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true, // for dev
                    ValidateAudience = true, // for dev
                    RequireExpirationTime = true, // for dev
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
                };
            });

        return services;
    }
}
