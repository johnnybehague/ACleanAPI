using ACleanAPI.Domain;
using ACleanAPI.Infrastructure;
using FluentResults;

namespace ACleanAPI.Application;

public class AcGetEntityByIdQueryHandlerBase<TEntity, TDto> // : IRequestHandler<IAcEntitiesRequest<TDto>, Result<IEnumerable<TDto>>>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    private readonly IAcGetEntityByIdRepository<TEntity> _repository;
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    protected AcGetEntityByIdQueryHandlerBase(IAcGetEntityByIdRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<TDto>> HandleRequest(IAcGetEntityByIdRequest<TDto> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetEntityByIdAsync(request.Id, cancellationToken);
        var dto = _mapper.MapToDto(entity);
        return Result.Ok(dto);
    }
}
