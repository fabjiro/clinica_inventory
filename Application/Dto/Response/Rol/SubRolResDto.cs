using Application.Dto.Request.Rol;
using Domain.Entities;

namespace Application.Dto.Response.Rol;

public class SubRolResDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = String.Empty;

    public RolResDto? Rol { get; set; }

    public List<PageEntity>? Pages { get; set; }
}