using FluentResults;
using LiteBus.Commands.Abstractions;

namespace ACleanAPI.Application.Commands;

/// <summary>
/// Command to delete an existing entity. 
/// </summary>
/// <typeparam name="TDto">DTO</typeparam>
/// <param name="Id">Id of the existing entity</param>
public record AcDeleteEntityCommand(int? Id) : ICommand<Result>;
