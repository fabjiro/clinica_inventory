using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("rol")]
public class RolEntity {

    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("name")]
    public string Name { get; set; } = String.Empty;
}