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

    [Required]
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public UserEntity? User { get; set; }

    [ForeignKey("RolId")]
    public RolEntity? Rol { get; set; }

    //  public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}