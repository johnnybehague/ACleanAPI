using ACleanAPI.Application.Interfaces;
using ACleanAPI.Domain.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.QueryHandlers;

public class AcGetEntityByIdQueryHandlerBase<TEntity, TDto> // : IRequestHandler<IAcEntitiesRequest<TDto>, Result<IEnumerable<TDto>>>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    private readonly IAcEntityRepository<TEntity> _repository;
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    protected AcGetEntityByIdQueryHandlerBase(IAcEntityRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<TDto?>> HandleRequest(IAcGetEntityByIdRequest<TDto> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetEntityByIdAsync(request.Id, cancellationToken);
        TDto? dto = !object.Equals(entity, default(TEntity)) ? _mapper.MapToDto(entity) : default;
        return Result.Ok(dto);
    }
}
