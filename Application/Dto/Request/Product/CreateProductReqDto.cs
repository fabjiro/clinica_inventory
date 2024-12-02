using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Product;

public class CreateProductReqDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; } = 0;
    
    public int Stock { get; set; } = 0;

    [Required(ErrorMessage = "CategoryId is required")]
    public string CategoryId { get; set; } = string.Empty;

    public string? Attribute { get; set; }

    [Display(Name = "Image")]
    public string? Image { get; set; }
}