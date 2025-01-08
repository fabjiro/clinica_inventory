using Application.Dto.Request.Rol;
using Application.Dto.Response.CivilStatus;
using Application.Dto.Response.Image;

namespace Application.Dto.Response.Patient;

public class PatientResDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public RolResDto? Rol { get; set; }
    public ImageResDto? Avatar { get; set; }
    public string? Identification { get; set; }
    public string? Phone { get; set; } = null;
    public string? Address { get; set; } = null;
    public int? Age { get; set; } = null;
    public int ConsultCount { get; set; }
    public string? ContactPerson { get; set; } = null;
    public string? ContactPhone { get; set; } = null;
    public DateTime? CreatedAt { get; set; } = null;
    public DateTime? Birthday { get; set; } = null;
    public Guid? TypeSex { get; set; } = null;
    public CivilStatusResDto? CivilStatus { get; set; }
}
