using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Interfaces;

public interface IAcDeleteEntityRequest : IRequest<Result>
{
    public int? Id { get; set; }
}
