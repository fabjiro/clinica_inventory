using System.ComponentModel.DataAnnotations;
namespace Application.Dto.Request.Category;

public class CategoryRequestDto {
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = String.Empty;
}