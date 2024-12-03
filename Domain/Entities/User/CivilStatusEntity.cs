using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("civil_status")]
public class CivilStatusEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;

    public CivilStatusEntity() {}

    public CivilStatusEntity(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}