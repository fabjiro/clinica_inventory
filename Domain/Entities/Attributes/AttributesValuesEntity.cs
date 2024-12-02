using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;

namespace Domain.Entities.Attributes;

[Table("attributes_values")]
public class AttributesValuesEntity : BaseEntity
{
    [Required]
    public Guid AttributeId { get; set; }

    [ForeignKey("AttributeId")]
    public AttributesEntity? Atribute { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public AttributesValuesEntity() { }

    public AttributesValuesEntity(Guid attributeId, string name)
    {
        Id = Guid.NewGuid();
        AttributeId = attributeId;
        Name = name;
    }

}