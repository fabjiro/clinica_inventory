using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;
using Domain.Const;
using Domain.Entities.Shop;

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

    public Guid? ShopId { get; set; } 

    [ForeignKey("ShopId")]
    public ShopEntity? Shop { get; set; }

    [Required]
    public Guid AvatarId { get; set; }

    [Required]
    [ForeignKey("AvatarId")]
    public ImageEntity? Avatar { get; set; }

    [Required]
    public Guid StatusId { get; set; }

    [Required]
    [ForeignKey("StatusId")]
    public StatusEntity? Status { get; set; }

    public Guid RolId { get; set; }

    [Required]
    [ForeignKey("RolId")]
    public RolEntity? Rol { get; set; }

    public UserEntity() { }
    public UserEntity(string name, string email, string password, Guid? rolId = null, Guid? statusId = null, Guid? avatarId = null, Guid? shopId = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        RolId = rolId ?? Guid.Parse(RolConst.Consultation);
        StatusId = statusId ?? Guid.Parse(StatusConst.Active);
        AvatarId = avatarId ?? Guid.Parse(DefaulConst.DefaultAvatarUserId);
        ShopId = shopId;
    }
}