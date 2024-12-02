using Application.Dto.Request.Rol;
using Application.Dto.Response.Image;

namespace Application.Dto.Response.User;

public class UserBasicResDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public RolResDto? Rol { get; set; }
    public ImageResDto? Avatar { get; set; }

}
