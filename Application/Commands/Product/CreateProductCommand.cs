using Application.Dto.Response.Product;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Product;

public record CreateProductCommand(string UserId, string Name, decimal Price, int Stock, Guid CategoryId, string? ImageBase64 = null,  string? Description = null, Guid? Attribute = null) : IRequest<Result<ProductBasicResDto>>;