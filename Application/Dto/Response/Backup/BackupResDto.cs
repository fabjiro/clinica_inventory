namespace Application.Dto.Response.Backup;

public class BackupResDto
{
    public string Id { get; set; } = String.Empty;
    public string Name { get; set; } = String.Empty;
    public string Url { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; }
}