using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Request.Consult;

public class UpdateConsultDtoReq
{
    [Required(ErrorMessage = "Id is required")]
    public string Id { get; set; } = string.Empty;
    public string? Patient { get; set; }

    public string? Motive { get; set; }

    public string? AntecedentPerson { get; set; }

    public string? Diagnostic { get; set; }

    public string? Recipe { get; set; }

    public DateTime? Nextappointment { get; set; }

    public decimal? Weight { get; set; }

    public decimal? Size { get; set; }

    public string? AntecedentFamily { get; set; }
    public string? Clinicalhistory { get; set; }
    public string? BilogicalEvaluation { get; set; }
    public string? PsychologicalEvaluation { get; set; }
    public string? SocialEvaluation { get; set; }
    public string? FunctionalEvaluation { get; set; }
    public decimal? Pulse { get; set; }
    public decimal? OxygenSaturation { get; set; }
    public decimal? SystolicPressure { get; set; }
    public decimal? DiastolicPressure { get; set; }
    public string? ImageExam { get; set; }
    public string? ExamComplementary { get; set; }
}