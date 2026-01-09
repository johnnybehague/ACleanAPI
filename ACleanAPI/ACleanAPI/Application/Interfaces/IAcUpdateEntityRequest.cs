using ACleanAPI.Application.Core;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ACleanAPI.Application.Interfaces;

public record AcUpdateEntityRequest<TDto>([BindRequired] int Id, TDto? Dto): IRequest<Result>
    where TDto : AcEntityDtoBase;
