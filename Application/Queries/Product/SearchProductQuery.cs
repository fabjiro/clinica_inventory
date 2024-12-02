using Application.Dto.Response.Product;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Product;

public record SearchProductQuery(string UserId, string Word) : IRequest<Result<List<ProductBasicResDto>>> {};