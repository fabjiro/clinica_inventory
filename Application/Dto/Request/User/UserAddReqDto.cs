using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.User;

public class UserAddReqDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;
    public string? Password { get; set; }
    public string? Rol { get; set; }
    public string? Avatar { get; set; }

}