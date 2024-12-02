using Application.Dto.Response.Product;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Product;

public record GetAllProductQuery(string UserId) : IRequest<Result<List<ProductBasicResDto>>> {};