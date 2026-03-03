using ACleanAPI.Application.Interfaces;
using FluentResults;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Application.Queries;

/// <summary>
/// Query to get all entities of a specific type.
/// </summary>
/// <typeparam name="T">Entity</typeparam>
public record AcGetEntitiesQuery<T> : IQuery<Result<IEnumerable<T>>>
    where T : IAcEntityDto;
