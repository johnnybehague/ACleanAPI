using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ACleanAPI.Application;

/// <summary>
/// Provides extension methods for registering application-level dependencies and modules with the dependency injection
/// container.
/// </summary>
/// <remarks>
/// This class contains static methods for configuring command and query modules using assemblies,
/// enabling the application to register its mediator handlers and related services. The extension methods are intended
/// to be used during application startup to set up required services for command and query processing.
/// </remarks>
[ExcludeFromCodeCoverage]
public static class ApplicationDependencyInjection
{
    /// <summary>
    /// Add application-level services and modules to the dependency injection container using a single assembly for both.
    /// </summary>
    /// <param name="services">A collection of services for the application to compose. This is useful for adding user provided or framework provided services.</param>
    /// <param name="mediatorAssembly"><see cref="Assembly" /> mediator.</param>
    /// <returns>
    /// A <see cref="IServiceCollection" /> that can be used to further configurations.
    //</returns>
    public static IServiceCollection AddAcApplication(this IServiceCollection services, Assembly mediatorAssembly)
    {
        services.AddLiteBus(liteBus =>
        {
            liteBus.AddCommandModule(module =>
            {
                module.RegisterFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
                module.RegisterFromAssembly(mediatorAssembly);
            });

            liteBus.AddQueryModule(module =>
            {
                module.RegisterFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
                module.RegisterFromAssembly(mediatorAssembly);
            });
        });

        return services;
    }

    /// <summary>
    /// Add application-level services and modules to the dependency injection container using several assemblies for commands and queries separately.
    /// </summary>
    /// <param name="services">A collection of services for the application to compose. This is useful for adding user provided or framework provided services.</param>
    /// <param name="queryMediatorAssembly"><see cref="Assembly" />  mediator for queries.</param>
    /// <param name="queryMediatorAssembly"><see cref="Assembly" />  mediator for commands.</param>
    /// <returns>
    /// A <see cref="IServiceCollection" /> that can be used to further configurations.
    //</returns>
    public static IServiceCollection AddAcApplication(this IServiceCollection services, Assembly queryMediatorAssembly, Assembly commandMediatorAssembly)
    {
        services.AddLiteBus(liteBus =>
        {
            liteBus.AddCommandModule(module =>
            {
                module.RegisterFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
                module.RegisterFromAssembly(commandMediatorAssembly);
            });

            liteBus.AddQueryModule(module =>
            {
                module.RegisterFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
                module.RegisterFromAssembly(queryMediatorAssembly);
            });
        });

        return services;
    }
}
