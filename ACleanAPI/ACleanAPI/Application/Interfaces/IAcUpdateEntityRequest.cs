using ACleanAPI.Application.Core;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ACleanAPI.Application.Interfaces;

public record AcUpdateEntityRequest<TDto>(int id, TDto? dto): IRequest<Result>
    where TDto : AcEntityDtoBase
{
    [BindRequired]
    public int Id { get; init; } = id;

    public TDto? Dto { get; init; } = dto;
}
