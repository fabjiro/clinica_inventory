using Application.Dto.Response.Image;
using Application.Dto.Response.Rol;

namespace Application.Dto.Response.User;

public class UserBasicResDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public SubRolResDto? Rol { get; set; }
    public ImageResDto? Avatar { get; set; }
    public List<string>? Routes { get; set; }
}
