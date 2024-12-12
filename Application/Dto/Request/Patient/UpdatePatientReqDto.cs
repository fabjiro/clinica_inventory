using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Patient;

public class UpdatePatientReqDto
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Identification { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? ContactPerson { get; set; }
    public string? ContactPhone { get; set; }
    public int? Age { get; set; }
    public DateTime? Birthday { get; set; }
    public string? TypeSex { get; set; }
    public string? CivilStatus { get; set; }
    public string? Avatar { get; set; }
}