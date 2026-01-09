using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Requests;

public record AcDeleteEntityRequest(int? Id) : IRequest<Result>;
