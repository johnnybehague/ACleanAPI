using ACleanAPI.Domain.Users.Interfaces;
using ACleanAPI.Infrastructure.Persistence;
using ACleanAPI.Infrastructure.Users.Mappers;
using ACleanAPI.Infrastructure.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ACleanAPI.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("TestDb"));


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserModelMapper, UserModelMapper>();

        return services;
    }
}
