using Application.Dto.Response.Attributes;
using Application.Dto.Response.Category;
using Application.Dto.Response.Image;

namespace Application.Dto.Response.Product;

public class ProductBasicResDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string? Description { get; set; }
    public int Stock { get; set; }
    public decimal Price { get; set; }
    public CategoryResDto? Category { get; set; }
    public ImageResDto? Image { get; set; }
    public AttributesBasicResDto? AttributeValue { get; set; }
}