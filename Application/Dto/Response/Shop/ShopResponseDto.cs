using Application.Dto.Response.Attributes;
using Application.Dto.Response.Image;

namespace Application.Dto.Response.Shop;

public class ShopResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int MinStockProducts { get; set; }
    public ImageResDto? Logo { get; set; }
    public ShopTypeResponseDto? ShopType { get; set; }
    public AttributesBasicResDto? Attribute { get; set; }
}