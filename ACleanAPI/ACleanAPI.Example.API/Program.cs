using ACleanAPI.Example.Application;
using ACleanAPI.Example.Infrastructure;
using ACleanAPI.Example.Infrastructure.Models;
using ACleanAPI.Example.Infrastructure.Persistence;
using ACleanAPI.Presentation;
using Asp.Versioning;
using System.Diagnostics.CodeAnalysis;

namespace ACleanAPI.Example.API;

[ExcludeFromCodeCoverage]
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();
        builder.Services.AddAcPresentation();

        // Versionning
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Supprime et recrée la base InMemory
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Seed custom
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new UserModel { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@email.com" },
                    new UserModel { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com" }
                );

                context.SaveChanges();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();

        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
