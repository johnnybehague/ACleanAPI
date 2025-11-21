using FluentResults;
using MediatR;

namespace ACleanAPI.Application;

public interface IAcGetEntitiesRequest<T> : IRequest<Result<IEnumerable<T>>>
    where T : IAcEntityDto
{
}
