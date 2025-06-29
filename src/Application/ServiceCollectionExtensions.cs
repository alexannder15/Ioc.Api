using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationCustomExtensions(
        this IServiceCollection services
    )
    {
        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IIocService, IocService>();

        return services;
    }
}
