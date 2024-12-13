using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.User;

public class UserUpdateReqDto
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Password { get; set; }
    public Guid? Rol { get; set; }
    public string? Avatar { get; set; }
}