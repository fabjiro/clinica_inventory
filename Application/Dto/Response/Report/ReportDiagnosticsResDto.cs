using Application.Dto.Response.User;

namespace Application.Dto.Response.Report;

public class DiagnosticsResDto 
{
    public string Name { get; set; } = string.Empty;
    public string Diagnostic { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserBasicResDto? UserCreatedBy { get; set; }

}