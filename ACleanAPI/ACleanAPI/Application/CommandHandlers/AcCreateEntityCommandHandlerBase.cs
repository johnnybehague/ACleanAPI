using ACleanAPI.Application.Commands;
using ACleanAPI.Application.Core;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Domain.Core;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.CommandHandlers;

public class AcCreateEntityCommandHandlerBase<TDto, TEntity>
    where TEntity : AcEntityBase
    where TDto: AcEntityDtoBase
{
    private readonly IAcEntityRepository<TEntity> _repository;
    private readonly IAcEntityMapper<TEntity, TDto> _mapper;

    protected AcCreateEntityCommandHandlerBase(IAcEntityRepository<TEntity> repository, IAcEntityMapper<TEntity, TDto> mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result> HandleRequest(AcCreateEntityCommand<TDto> request, CancellationToken cancellationToken)
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
