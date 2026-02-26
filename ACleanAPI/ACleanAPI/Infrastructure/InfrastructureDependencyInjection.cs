using ACleanAPI.Application.Interfaces;
using ACleanAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ACleanAPI.Infrastructure;

[ExcludeFromCodeCoverage]
public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddAcInfrastructure<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptionsAction)
        where TContext: DbContext
    {
        services.AddDbContext<TContext>(dbContextOptionsAction);
        services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

        return services;
    }
}
