namespace Application.Dto.Response.Report;
public class ReportRecentConsultResDto
{
    public string? PatientName { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Diagnostic { get; set; }
    public string? Motive { get; set; } = string.Empty;

}