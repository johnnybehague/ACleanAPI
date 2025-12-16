using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Interfaces;

public interface IAcGetEntitiesRequest<T> : IRequest<Result<IEnumerable<T>>>
    where T : IAcEntityDto
{
}
