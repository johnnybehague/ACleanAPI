using ACleanAPI.Application.Core;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Interfaces;

public record AcUpdateEntityRequest<TDto>(int Id, TDto? Dto): IRequest<Result>
    where TDto : AcEntityDtoBase;
