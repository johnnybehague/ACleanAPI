using ACleanAPI.Example.Domain.Users.Interfaces;
using ACleanAPI.Example.Infrastructure.Persistence;
using ACleanAPI.Example.Infrastructure.Users.Mappers;
using ACleanAPI.Example.Infrastructure.Users.Repositories;
using ACleanAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ACleanAPI.Example.Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAcInfrastructure<AppDbContext>(options => options.UseInMemoryDatabase("TestDb"));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserModelMapper, UserModelMapper>();

        return services;
    }
}
