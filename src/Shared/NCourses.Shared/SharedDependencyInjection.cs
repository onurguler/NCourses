using Microsoft.Extensions.DependencyInjection;

using NCourses.Shared.Services;

namespace NCourses.Shared;

public static class SharedDependencyInjection
{
    public static IServiceCollection AddSharedDependencies(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ISharedIdentityService, SharedIdentityService>();

        return services;
    }
}