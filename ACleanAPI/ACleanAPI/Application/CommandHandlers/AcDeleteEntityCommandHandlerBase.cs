using ACleanAPI.Application.Commands;
using ACleanAPI.Domain.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.CommandHandlers;

/// <summary>
/// Base class for Deleting Entity Command Handler. This class provides common logic for handling delete commands for entities. 
/// </summary>
/// <remarks>
/// It uses a repository to delete the entity.
/// </remarks>
/// <typeparam name="TEntity">Entity</typeparam>
public class AcDeleteEntityCommandHandlerBase<TEntity>
    where TEntity : IAcEntity
{
    /// <summary>
    /// Repository for managing entities. This is used to delete the specified entity in the database.
    /// </summary>
    private readonly IAcEntityRepository<TEntity> _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcDeleteEntityCommandHandlerBase{TEntity}"/> class with the specified entity repository.
    /// </summary>
    /// <remarks>— 
    /// This constructor is intended for use by derived classes to provide the necessary dependencies for entity deleting operations.</remarks>
    /// <param name="repository">The repository used to delete <typeparamref name="TEntity"/> instances.</param>
    protected AcDeleteEntityCommandHandlerBase(IAcEntityRepository<TEntity> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Asynchronously handles the deletion of a new entity based on the provided command.
    /// </summary>
    /// <param name="request">Request to delete <typeparamref name="TEntity"/></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation.</returns>
    public async Task<Result> HandleCommandAsync(AcDeleteEntityCommand request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Result.Fail("Id is required.");

        try
        {
            await _repository.DeleteEntityAsync(request.Id.Value, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error deleting entity: {ex.Message}");
        }
        
        return Result.Ok();
    }
}
