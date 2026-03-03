using ACleanAPI.Application.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Requests;

public record AcGetEntityByIdRequest<T>(int? Id) : IRequest<Result<T>>
    where T : IAcEntityDto;
