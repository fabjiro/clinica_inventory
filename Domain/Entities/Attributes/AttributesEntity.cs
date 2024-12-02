using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;

namespace Domain.Entities.Attributes;

[Table("attribute")]
public class AttributesEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public AttributesEntity() { }

    public AttributesEntity(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
}