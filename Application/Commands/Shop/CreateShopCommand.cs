using Application.Dto.Response.Shop;
using Ardalis.Result;
using MediatR;

namespace Application.Commands.Shop;

public record CreateShopCommand(string UserId, string Name,int MinStockProducts, Guid ShopTypeId, Guid AttributeId, string? Image) : IRequest<Result<ShopResponseDto>>;