using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;
using Domain.Const;


namespace Domain.Entities;

[Table("patient")]
public class PatientEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Identification { get; set; } = null;
    public string? Phone { get; set; } = null;
    public string? Address { get; set; } = null;
    public int? Age { get; set; } = null;
    public int ConsultCount { get; set; } = 0;
    public string? ContactPerson { get; set; } = null;
    public string? ContactPhone { get; set; } = null;
    public DateTime? Birthday { get; set; } = null;
    public Guid? TypeSex { get; set; } = null;

    public Guid? CivilStatusId { get; set; }

    [ForeignKey("CivilStatusId")]
    public CivilStatusEntity? CivilStatus { get; set; }

    [Required]
    public Guid AvatarId { get; set; }

    [Required]
    [ForeignKey("AvatarId")]
    public ImageEntity? Avatar { get; set; }

    public Guid RolId { get; set; }

    [Required]
    [ForeignKey("RolId")]
    public RolEntity? Rol { get; set; }

    public PatientEntity() { }

    public PatientEntity(string name, Guid? rolId = null, Guid? avatarId = null, Guid? civilStatusId = null, Guid? typeSex = null, string? identification = null, string? phone = null, string? address = null, int? age = null, string? contactPerson = null, string? contactPhone = null, DateTime? birthday = null, int consultCount = 0)
    {
        Id = Guid.NewGuid();
        Name = name;
        RolId = rolId ?? Guid.Parse(RolConst.Consultation);
        AvatarId = avatarId ?? Guid.Parse(DefaulConst.DefaultAvatarUserId);
        CivilStatusId = civilStatusId;
        TypeSex = typeSex;
        Identification = identification;
        Phone = phone;
        Address = address;
        Age = age;
        ContactPerson = contactPerson;
        ContactPhone = contactPhone;
        Birthday = DateTime.SpecifyKind(birthday ?? DateTime.UtcNow, DateTimeKind.Utc);
        ConsultCount = consultCount;
    }
}