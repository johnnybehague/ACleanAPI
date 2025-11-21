using FluentResults;
using MediatR;

namespace ACleanAPI.Application;

public interface IAcEntitiesRequest<T> : IRequest<Result<IEnumerable<T>>>
    where T : IAcEntityDto
{
}
