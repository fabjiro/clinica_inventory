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

    [EmailAddress]
    public string? Email { get; set; } = null;

    public string? Identification { get; set; } = null;
    public string? Phone { get; set; } = null;
    public string? Address { get; set; } = null;
    public int? Age { get; set; } = null;
    public string? ContactPerson { get; set; } = null;
    public string? ContactPhone { get; set; } = null;
    public DateTime? Birthday { get; set; } = null;
    public Guid? TypeSex { get; set; } = null;

    public Guid? CivilStatusId { get; set; }

    [ForeignKey("CivilStatusId")]
    public CivilStatusEntity? CivilStatus { get; set; }


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

    public Guid RolId { get; set; }

    [Required]
    [ForeignKey("RolId")]
    public RolEntity? Rol { get; set; }

    public UserEntity() { }
    public UserEntity(string name, string email, string password, Guid? rolId = null, Guid? avatarId = null, Guid? shopId = null, Guid? civilStatusId = null, Guid? typeSex = null, string? identification = null, string? phone = null, string? address = null, int? age = null, string? contactPerson = null, string? contactPhone = null, DateTime? birthday = null)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        RolId = rolId ?? Guid.Parse(RolConst.Consultation);
        AvatarId = avatarId ?? Guid.Parse(DefaulConst.DefaultAvatarUserId);
        ShopId = shopId;
        CivilStatusId = civilStatusId;
        TypeSex = typeSex;
        Identification = identification;
        Phone = phone;
        Address = address;
        Age = age;
        ContactPerson = contactPerson;
        ContactPhone = contactPhone;
        Birthday = birthday;
    }
}