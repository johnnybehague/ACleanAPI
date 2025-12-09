using ACleanAPI.Example.Domain.Users.Interfaces;
using ACleanAPI.Example.Infrastructure.Persistence;
using ACleanAPI.Example.Infrastructure.Users.Mappers;
using ACleanAPI.Example.Infrastructure.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ACleanAPI.Example.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"));


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserDetailRepository, UserDetailRepository>();
        services.AddScoped<IUserModelMapper, UserModelMapper>();

        return services;
    }
}
