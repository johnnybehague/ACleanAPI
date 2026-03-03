using ACleanAPI.Application.Core;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Requests;

public record AcCreateEntityRequest<TDto>(TDto? Dto) : IRequest<Result>
    where TDto : AcEntityDtoBase;
