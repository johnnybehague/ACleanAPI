using LiteBus.Commands;
using LiteBus.Extensions.Microsoft.DependencyInjection;
using LiteBus.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ACleanAPI.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationDependencyInjection
{
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
