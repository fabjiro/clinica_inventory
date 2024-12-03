using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.User;

public class AddPatientReqDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Identification is required")]
    public string Identification { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone is required")]
    public string Phone { get; set; } = string.Empty;

    public string? Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Age is required")]    
    public int Age { get; set; }

    [Required(ErrorMessage = "ContactPerson is required")]    
    public string ContactPerson { get; set; } = string.Empty;
    [Required(ErrorMessage = "ContactPhone is required")]    
    public string ContactPhone { get; set; } = string.Empty;
    [Required(ErrorMessage = "Birthday is required")]    
    public DateTime? Birthday { get; set; } = null;
    [Required(ErrorMessage = "TypeSex is required")]    
    public string TypeSex { get; set; } = string.Empty;
    [Required(ErrorMessage = "CivilStatus is required")]    
    public string CivilStatus { get; set; } = string.Empty;
    public string? Avatar { get; set; }
}