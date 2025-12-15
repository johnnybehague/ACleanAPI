using FluentResults;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ACleanAPI.Application.Interfaces;

public interface IAcGetEntityByIdRequest<T> : IRequest<Result<T?>>
    where T : IAcEntityDto
{
    [Required]
    public int Id { get; set; }
}
