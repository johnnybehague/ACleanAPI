using ACleanAPI.Application.Core;
using FluentResults;
using MediatR;
using System.Text.Json.Serialization;

namespace ACleanAPI.Application.Requests;

public record AcUpdateEntityRequest<TDto>([property: JsonRequired] int Id, TDto? Dto): IRequest<Result>
    where TDto : AcEntityDtoBase;
