using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Interfaces;

public interface IAcGetEntityByIdRequest<T> : IRequest<Result<T>>
    where T : IAcEntityDto
{
    public int? Id { get; set; }
}
