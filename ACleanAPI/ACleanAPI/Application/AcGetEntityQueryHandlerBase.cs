using ACleanAPI.Domain;
using ACleanAPI.Infrastructure;
using FluentResults;

namespace ACleanAPI.Application;

public class AcGetEntityQueryHandlerBase<TEntity, TDto> // : IRequestHandler<IAcEntitiesRequest<TDto>, Result<IEnumerable<TDto>>>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    private readonly IAcGetDetailRepository<TEntity> _repository;
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    protected AcGetEntityQueryHandlerBase(IAcGetDetailRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<TDto>> HandleRequest(IAcEntityRequest<TDto> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken); // A revoir pour passer en GetAll
        var dto = _mapper.MapToDto(entity);
        return Result.Ok(dto);
    }
}
