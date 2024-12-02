namespace  Application.Dto.Response.Image;

public class ImageResDto
{
    public Guid Id { get; set; }
    public string OriginalUrl { get; set; } = string.Empty;
    public string CompactUrl { get; set; } = string.Empty;
}