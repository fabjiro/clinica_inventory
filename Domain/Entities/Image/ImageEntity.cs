using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("image")]
public class ImageEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string OriginalUrl { get; set; } = string.Empty;

    [Required]
    public string CompactUrl { get; set; } = string.Empty;

    public ImageEntity() { }

    public ImageEntity(string originalUrl, string compactUrl)
    {
        OriginalUrl = originalUrl;
        CompactUrl = compactUrl;
    }
}