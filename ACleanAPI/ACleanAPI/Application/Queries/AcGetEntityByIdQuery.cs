using ACleanAPI.Application.Interfaces;
using FluentResults;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Application.Queries;

public record AcGetEntityByIdQuery<T>(int? Id) : IQuery<Result<T>>
    where T : IAcEntityDto;
