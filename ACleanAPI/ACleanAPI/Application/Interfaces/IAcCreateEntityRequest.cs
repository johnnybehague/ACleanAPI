using ACleanAPI.Application.Core;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Interfaces;

public interface IAcCreateEntityRequest<TDto> : IRequest<Result>
    where TDto : AcEntityDtoBase
{
    public TDto? Dto { get; set; }
}
