using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;
using Domain.Const;

namespace Domain.Entities;

[Table("user")]
public class UserEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public Guid AvatarId { get; set; }

    [Required]
    [ForeignKey("AvatarId")]
    public ImageEntity? Avatar { get; set; }

    public Guid SubRolId { get; set; }

    [Required]
    [ForeignKey("SubRolId")]
    public SubRolEntity? Rol { get; set; }

    public UserEntity() { }
    public UserEntity(string name,string email, string password, Guid rolId, Guid? avatarId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        SubRolId = rolId;
        AvatarId = avatarId ?? Guid.Parse(DefaulConst.DefaultAvatarUserId);
    }
}