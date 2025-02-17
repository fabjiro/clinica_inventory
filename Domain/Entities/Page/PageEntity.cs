using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("page")]
public class PageEntity
{   
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Url { get; set; } = string.Empty;
}