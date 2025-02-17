using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("sub_rol")]
public class SubRolEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("name")]
    public string Name { get; set; } = String.Empty;

    // que tipo de rol es
    [Required]
    public Guid RolId { get; set; }

    [ForeignKey("RolId")]
    public RolEntity? Rol { get; set; }

    public SubRolEntity() {}
    public SubRolEntity(
        string name,
        Guid rolId
    ) {
        Id = Guid.NewGuid();
        Name = name;
        RolId = rolId;
    }
    //  public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}