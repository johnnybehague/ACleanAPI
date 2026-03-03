using ACleanAPI.Presentation.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ACleanAPI.Presentation;

[ExcludeFromCodeCoverage]
public static class PresentationDependencyInjection
{
    public static IServiceCollection AddAcPresentation(this IServiceCollection services)
    {
        services.AddScoped<IAcMediator, AcMediator>();

        return services;
    }
}
