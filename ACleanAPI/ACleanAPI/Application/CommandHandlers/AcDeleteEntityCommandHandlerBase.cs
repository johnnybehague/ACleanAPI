using ACleanAPI.Application.Requests;
using ACleanAPI.Domain.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using FluentResults;

namespace ACleanAPI.Application.CommandHandlers;

public class AcDeleteEntityCommandHandlerBase<TEntity>
    where TEntity : IAcEntity
{
    private readonly IAcEntityRepository<TEntity> _repository;

    protected AcDeleteEntityCommandHandlerBase(IAcEntityRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<Result> HandleRequest(AcDeleteEntityRequest request, CancellationToken cancellationToken)
    {
        if (!request.Id.HasValue)
            return Result.Fail("Id is required.");

        try
        {
            await _repository.DeleteEntityAsync(request.Id.Value, cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Fail($"Error deleting entity: {ex.Message}");
        }
        
        return Result.Ok();
    }
}
