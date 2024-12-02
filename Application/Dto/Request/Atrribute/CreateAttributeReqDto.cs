using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Attribute;

public class CreateAttributeReqDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
}