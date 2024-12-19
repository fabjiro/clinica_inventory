using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.BaseClass;

namespace Domain.Entities;

[Table("exam")]
public class ExamEntity : BaseEntity
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public Guid GroupId { get; set; }

    [ForeignKey("GroupId")]
    public GroupEntity? Group { get; set; } = null;

    public ExamEntity() {}

    public ExamEntity(string name, Guid groupId)
    {
        Id = Guid.NewGuid();
        Name = name;
        GroupId = groupId;
    }
}