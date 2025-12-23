using ACleanAPI.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ACleanAPI.Application;

[ExcludeFromCodeCoverage]
public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddAcApplication(this IServiceCollection services, params Assembly[] mediatorAssemblies)
    {
        // Configuration par défaut de MediatR avec les pipelines
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
            cfg.RegisterServicesFromAssemblies(mediatorAssemblies);
        });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        return services;
    }
}
