using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ACleanAPI.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure-level dependencies and modules with the dependency injection
/// container.
/// </summary>
/// <remarks>
/// This class contains static methods for configuring repositories and contexts using assemblies
/// </remarks>
[ExcludeFromCodeCoverage]
public static class InfrastructureDependencyInjection
{
    /// <summary>
    /// Add infrastructure-level services and modules to the dependency injection container using a <see cref="DbContextOptionsBuilder" />.
    /// </summary>
    /// <param name="services">A collection of services for the infrastructre to compose.</param>
    /// <param name="mediatorAssembly"><see cref="DbContextOptionsBuilder" /> builder.</param>
    /// <returns>
    /// A <see cref="IServiceCollection" /> that can be used to further configurations.
    //</returns>
    public static IServiceCollection AddAcInfrastructure<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextOptionsAction)
        where TContext: DbContext
    {
        services.AddDbContext<TContext>(dbContextOptionsAction);

        return services;
    }
}
