using ACleanAPI.Application;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Application.Users.Queries.GetUsers;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ACleanAPI.Example.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Injection Application
        services.AddAcApplication(typeof(GetUsersQuery).Assembly);

        // Users
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IUserDetailMapper, UserDetailMapper>();
        return services;
    }
}
