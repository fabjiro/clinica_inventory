using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("backup")]
public class BackupEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = String.Empty;

    [Required]
    public string Url { get; set; } = String.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public BackupEntity() { }

    public BackupEntity(string name, string url)
    {
        Name = name;
        Url = url;
    }
}