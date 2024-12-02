using Application.Dto.Response.Product;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Product;

public record UpdateProductCommand(
    string UserId,
    Guid Id,
    string? Name,
    string? Description,
    decimal? Price,
    int? Stock,
    Guid? CategoryId,
    string? Image
) : IRequest<Result<ProductBasicResDto>> {};