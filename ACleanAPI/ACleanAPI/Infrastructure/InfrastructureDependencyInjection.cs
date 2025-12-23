using ACleanAPI.Application.Interfaces;
using ACleanAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ACleanAPI.Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddAcInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptionsAction)
    {
        services.AddDbContext<DbContext>(dbContextOptionsAction);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
