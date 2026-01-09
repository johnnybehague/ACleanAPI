using ACleanAPI.Application.Core;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Interfaces;

//public interface IAcUpdateEntityRequest<TDto> : IRequest<Result>
//    where TDto : AcEntityDtoBase
//{
//    public int Id { get; set; }

//    public TDto? Dto { get; set; }
//}

public record AcUpdateEntityRequest<TDto>(int Id, TDto? Dto): IRequest<Result>
    where TDto : AcEntityDtoBase;
