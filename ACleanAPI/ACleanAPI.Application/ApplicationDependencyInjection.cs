using ACleanAPI.Application.Users.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace ACleanAPI.Application;

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