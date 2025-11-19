using ACleanAPI.Application;
using ACleanAPI.Application.Users.Queries.GetUsers;
using ACleanAPI.Infrastructure;
using ACleanAPI.Infrastructure.Models;
using ACleanAPI.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblies(
        typeof(GetUsersQuery).Assembly
    ));

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
