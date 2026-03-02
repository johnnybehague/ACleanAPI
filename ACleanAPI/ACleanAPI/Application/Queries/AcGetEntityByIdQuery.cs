using ACleanAPI.Application.Interfaces;
using FluentResults;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Application.Queries;

/// <summary>
/// Query to get a single entity by its ID.
/// </summary>
/// <typeparam name="T">Entity</typeparam>
/// <param name="Id">Id of the entity</param>
public record AcGetEntityByIdQuery<T>(int? Id) : IQuery<Result<T>>
    where T : IAcEntityDto;
