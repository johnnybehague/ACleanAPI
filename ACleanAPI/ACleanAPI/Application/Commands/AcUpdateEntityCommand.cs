using ACleanAPI.Application.Core;
using FluentResults;
using LiteBus.Commands.Abstractions;
using System.Text.Json.Serialization;

namespace ACleanAPI.Application.Commands;

/// <summary>
/// Command to delete an existing entity. 
/// </summary>
/// <typeparam name="TDto">DTO</typeparam>
/// <param name="Id">Id of the existing entity</param>
/// <param name="Dto">DTO</param>
public record AcUpdateEntityCommand<TDto>([property: JsonRequired] int Id, TDto? Dto): ICommand<Result>
    where TDto : AcEntityDtoBase;
