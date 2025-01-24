namespace Application.Dto.Response.Report;

public class ReportNextConsultsResDto
{
    public string PatientName { get; set; } = string.Empty;
    public string PatientPhone { get; set; } = string.Empty;
    public string ContactPerson { get; set; } = string.Empty;
    public DateTime? NextAppointment { get; set; }
}