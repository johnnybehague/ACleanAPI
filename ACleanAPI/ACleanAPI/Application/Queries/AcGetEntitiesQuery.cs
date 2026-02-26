using ACleanAPI.Application.Interfaces;
using FluentResults;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Application.Queries;

public record AcGetEntitiesQuery<T> : IQuery<Result<IEnumerable<T>>>
    where T : IAcEntityDto;
