using FluentResults;
using LiteBus.Commands.Abstractions;

namespace ACleanAPI.Application.Commands;

public record AcDeleteEntityCommand(int? Id) : ICommand<Result>;
