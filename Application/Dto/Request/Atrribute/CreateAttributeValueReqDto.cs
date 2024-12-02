using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Attribute;

public class CreateAttributeValueReqDto
{
    [Required(ErrorMessage = "Attribute is required")]
    public string Attribute { get; set; } = string.Empty;

    [Required(ErrorMessage = "Value is required")]
    public string Value { get; set; } = string.Empty;
}