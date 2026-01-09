using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.Requests;
using ACleanAPI.Domain.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.QueryHandlers;

public class AcGetEntityByIdQueryHandlerBase<TEntity, TDto>
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

    public async Task<Result<TDto>> HandleRequest(AcGetEntityByIdRequest<TDto> request, CancellationToken cancellationToken)
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
