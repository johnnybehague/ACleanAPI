using ACleanAPI.Domain;
using ACleanAPI.Infrastructure;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application;

public abstract class AcGetEntitiesQueryHandlerBase<TEntity, TDto> // : IRequestHandler<IAcEntitiesRequest<TDto>, Result<IEnumerable<TDto>>>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    private readonly IAcGetEntitiesRepository<TEntity> _repository;
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    protected AcGetEntitiesQueryHandlerBase(IAcGetEntitiesRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<TDto>>> HandleRequest(IAcGetEntitiesRequest<TDto> request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetEntitiesAsync(cancellationToken);
        IEnumerable<TDto> dtos = entities.Select(_mapper.MapToDto).ToList();
        return Result.Ok(dtos);
    }
}
