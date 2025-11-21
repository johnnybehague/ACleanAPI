using FluentResults;
using MediatR;

namespace ACleanAPI.Application;

public interface IAcGetEntityByIdRequest<T> : IRequest<Result<T>>
    where T : IAcEntityDto
{
    public int Id { get; set; }
}
