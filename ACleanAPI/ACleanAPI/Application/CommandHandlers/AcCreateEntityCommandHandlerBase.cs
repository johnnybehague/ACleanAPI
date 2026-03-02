using ACleanAPI.Application.Commands;
using ACleanAPI.Application.Core;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Domain.Core;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.CommandHandlers;

/// <summary>
/// Base class for Creating Entity Command Handler. This class provides common logic for handling create commands for entities. 
/// </summary>
/// <remarks>
/// It uses a repository to persist the entity and a mapper to convert between DTOs and entities.
/// </remarks>
/// <typeparam name="TDto">DTO</typeparam>
/// <typeparam name="TEntity">Entity</typeparam>
public class AcCreateEntityCommandHandlerBase<TDto, TEntity>
    where TEntity : AcEntityBase
    where TDto: AcEntityDtoBase
{
    /// <summary>
    /// Repository for managing entities. This is used to persist the created entity to the database.
    /// </summary>
    private readonly IAcEntityRepository<TEntity> _repository;

    /// <summary>
    /// Mapper to convert between DTOs and entities. This is used to map the incoming DTO from the command to the entity that will be persisted.
    /// </summary>
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcCreateEntityCommandHandlerBase{TEntity, TDto}"/> class with the specified entity repository and mapper.
    /// </summary>
    /// <remarks>— 
    /// This constructor is intended for use by derived classes to provide the necessary dependencies for entity creation and mapping operations.</remarks>
    /// <param name="repository">The repository used to persist and retrieve <typeparamref name="TEntity"/> instances.</param>
    /// <param name="mapper">The mapper used to convert between <typeparamref name="TEntity"/> and <typeparamref name="TDto"/> objects.</param>
    protected AcCreateEntityCommandHandlerBase(IAcEntityRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Asynchronously handles the creation of a new entity based on the provided command. This method validates the input DTO, maps it to an entity, and persists it using the repository.
    /// </summary>
    /// <param name="request">Request to create <typeparamref name="TEntity"/></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation.</returns>
    public async Task<Result> HandleCommandAsync(AcCreateEntityCommand<TDto> request, CancellationToken cancellationToken)
    {
        if (request.Dto == null)
            return Result.Fail("Entity is required.");

        var entity = _mapper.MapToEntity(request.Dto);

        try
        {
            await _repository.CreateEntityAsync(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error creating entity: {ex.Message}");
        }

        return Result.Ok();
    }
}
