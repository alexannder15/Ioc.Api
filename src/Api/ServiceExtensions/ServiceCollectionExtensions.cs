namespace Api.ServiceExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
                policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );
        });

        return services;
    }
}
