using ACleanAPI.Example.Application.Users.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ACleanAPI.Example.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Users
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IUserDetailMapper, UserDetailMapper>();
        return services;
    }
}