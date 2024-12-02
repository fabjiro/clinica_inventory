using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Inventory;

public class AddInventoryReqDto
{
    [Required(ErrorMessage = "Product is required")]
    public string Product { get; set; } = string.Empty;

    [Required(ErrorMessage = "Count is required")]
    public int Count { get; set; } = 0;

    [Required(ErrorMessage = "TypeMovement is required")]
    public string TypeMovement { get; set; } = string.Empty;
}