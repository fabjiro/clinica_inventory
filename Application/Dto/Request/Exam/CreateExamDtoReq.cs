using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Exam;

public class CreateExamDtoReq
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Group is required")]
    public string Group { get; set; } = string.Empty;
}