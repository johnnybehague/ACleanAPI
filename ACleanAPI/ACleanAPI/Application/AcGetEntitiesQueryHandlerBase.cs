using ACleanAPI.Domain;
using ACleanAPI.Infrastructure;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application;

public abstract class AcGetEntitiesQueryHandlerBase<TEntity, TDto> // : IRequestHandler<IAcEntitiesRequest<TDto>, Result<IEnumerable<TDto>>>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    private readonly IAcGetAllRepository<TEntity> _repository;
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    protected AcGetEntitiesQueryHandlerBase(IAcGetAllRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<TDto>>> HandleRequest(IAcEntitiesRequest<TDto> request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken); // A revoir pour passer en GetAll
        IEnumerable<TDto> dtos = entities.Select(_mapper.MapToDto).ToList();
        return Result.Ok(dtos);
    }
}
