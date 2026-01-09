using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.Requests;
using ACleanAPI.Domain.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.QueryHandlers;

public abstract class AcGetEntitiesQueryHandlerBase<TEntity, TDto>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    private readonly IAcEntityRepository<TEntity> _repository;
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    protected AcGetEntitiesQueryHandlerBase(IAcEntityRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<TDto>>> HandleRequest(AcGetEntitiesRequest<TDto> request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetEntitiesAsync(cancellationToken);
        IEnumerable<TDto> dtos = entities.Select(_mapper.MapToDto).ToList();
        return Result.Ok(dtos);
    }
}
