using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Shop;

public class CreateShopReqDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "MinStockProducts is required")]
    public int MinStockProducts { get; set; }

    [Required(ErrorMessage = "ShopTypeId is required")]
    public string ShopType { get; set; } = null!;
    public string? Logo { get; set; }
}