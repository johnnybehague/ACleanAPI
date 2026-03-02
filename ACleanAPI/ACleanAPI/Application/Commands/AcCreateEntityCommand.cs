using ACleanAPI.Application.Core;
using FluentResults;
using LiteBus.Commands.Abstractions;

namespace ACleanAPI.Application.Commands;

/// <summary>
/// Command to create a new entity. 
/// </summary>
/// <typeparam name="TDto">DTO</typeparam>
/// <param name="Dto">DTO</param>
public record AcCreateEntityCommand<TDto>(TDto? Dto) : ICommand<Result>
    where TDto : AcEntityDtoBase;
