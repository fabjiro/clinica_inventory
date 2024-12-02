using Application.Dto.Response.Product;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Product;

public record DeleteProductCommand(string UserId, Guid Id) : IRequest<Result<ProductBasicResDto>>;