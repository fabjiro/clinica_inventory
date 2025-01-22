namespace Application.Dto.Response.Report;

public class ReportRegistrationByUserResDto
{
    public string UserName { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string Motive { get; set; } = string.Empty;
    public string Diagnostic { get; set; } = string.Empty;
}