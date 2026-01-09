using ACleanAPI.Application.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Requests;

public record AcGetEntitiesRequest<T> : IRequest<Result<IEnumerable<T>>>
    where T : IAcEntityDto;
