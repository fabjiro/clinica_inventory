using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("status")]
public class StatusEntity {

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("name")]
    public string Name { get; set; } = String.Empty;

    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();

}