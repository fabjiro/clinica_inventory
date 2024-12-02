using Application.Dto.Response.Shop;
using Ardalis.Result;
using MediatR;

namespace Application.Queries.Shop;

public record GetMeShopQuery(string UserId) : IRequest<Result<ShopResponseDto>> {};