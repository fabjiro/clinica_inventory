using Application.Dto.Response.Image;

namespace Application.Dto.Response.Product;

public class ProductSearchResDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public ImageResDto? Image { get; set; }
}