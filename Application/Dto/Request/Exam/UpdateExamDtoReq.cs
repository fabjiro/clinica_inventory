using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Exam;

public class UpdateExamDtoReq
{
    [Required]
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; }
    public string? Group { get; set; }
}