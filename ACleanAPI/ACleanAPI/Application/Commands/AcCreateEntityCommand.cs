using ACleanAPI.Application.Core;
using FluentResults;
using LiteBus.Commands.Abstractions;

namespace ACleanAPI.Application.Commands;

public record AcCreateEntityCommand<TDto>(TDto? Dto) : ICommand<Result>
    where TDto : AcEntityDtoBase;
