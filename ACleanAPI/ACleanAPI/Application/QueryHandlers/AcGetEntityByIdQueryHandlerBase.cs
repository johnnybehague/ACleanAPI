using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.Queries;
using ACleanAPI.Domain.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.QueryHandlers;

/// <summary>
/// Base class for Getting Single Entity Query Handler.
/// </summary>
/// <remarks>
/// This class provides common logic for handling getting single data queries for entities. 
/// </remarks>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TDto">DTO</typeparam>
public class AcGetEntityByIdQueryHandlerBase<TEntity, TDto>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    /// <summary>
    /// Repository for managing entities.
    /// </summary>
    /// <remarks>
    /// This is used to getting single entity from the database.
    /// </remarks>
    private readonly IAcEntityRepository<TEntity> _repository;

    /// <summary>
    /// Mapper to convert between entities and DTOs. 
    /// </summary>
    /// <remarks>
    /// This is used to map the incoming entity from the query to the DTO that will be manipulated.
    /// </remarks>
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AcGetEntityByIdQueryHandlerBase{TEntity, TDto}"/> class with the specified entity repository and mapper.
    /// </summary>
    /// <param name="repository">The repository used to retrieve <typeparamref name="TEntity"/> instances.</param>
    /// <param name="mapper">The mapper used to convert between <typeparamref name="TEntity"/> and <typeparamref name="TDto"/> objects.</param>
    protected AcGetEntityByIdQueryHandlerBase(IAcEntityRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Asynchronously handles the getting of single entity based on the provided query. 
    /// </summary>
    /// <remarks>
    /// This method get single entity and map it to DTO.
    /// </remarks>
    /// <param name="request">Request to create <typeparamref name="TEntity"/></param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the  <typeparamref name="TEntity"/> value.</returns>
    public async Task<Result<TDto>> HandleQueryAsync(AcGetEntityByIdQuery<TDto> request, CancellationToken cancellationToken)
    {
        if(!request.Id.HasValue)
            return Result.Fail("Id is required.");

        var entity = await _repository.GetEntityByIdAsync(request.Id.Value, cancellationToken);

        if(entity is null)
            return Result.Fail("Entity not found.");

        TDto dto =  _mapper.MapToDto(entity);
        return Result.Ok(dto);
    }
}
