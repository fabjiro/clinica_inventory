namespace Application.Dto.Response.Report;

public class ReportMasterResDto
{
    public DateTime? CreatedAt { get; set; }
    public string? NamePatient { get; set; }
    public string? PhonePatient { get; set; }
    public int? Age { get; set; }
    public string? Address { get; set; }
    public string? ContactName { get; set; }
    public string? ContactPhone { get; set; }
    public string? RegisterBy { get; set; }
    public int? CountConsult { get; set; }
}