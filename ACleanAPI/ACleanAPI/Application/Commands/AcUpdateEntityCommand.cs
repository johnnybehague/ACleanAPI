using ACleanAPI.Application.Core;
using FluentResults;
using LiteBus.Commands.Abstractions;
using System.Text.Json.Serialization;

namespace ACleanAPI.Application.Commands;

public record AcUpdateEntityCommand<TDto>([property: JsonRequired] int Id, TDto? Dto): ICommand<Result>
    where TDto : AcEntityDtoBase;
